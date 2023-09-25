using GameDevTV.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ItemsCategory { Weapon, Armor, Accessorie, Potion }

public class CharacterInventory : MonoBehaviour,IPredicateEvaluator
{
    public List<ItemsSlot> weaponSlots;
    [SerializeField] List<ItemsSlot> armorSlots;
    [SerializeField] List<ItemsSlot> accessorieSlots;
    [SerializeField] List<ItemsSlot> potionSlots;

    List<List<ItemsSlot>> allSlots;

    public event Action OnUpdated;

    private void Awake()
    {
        allSlots = new List<List<ItemsSlot>>() { weaponSlots, armorSlots, accessorieSlots, potionSlots };
    }

    public static List<string> ItemCategories { get; set; } = new List<string>()
    {
        "Weapon", "Armor", "Accessorie", "Potion"
    };

    public List<ItemsSlot> GetSlotsByCategory(int categoryIndex)
    {
        return allSlots[categoryIndex];
    }

    public ItemBase GetItem(int itemIndex, int categoryIndex)
    {
        var currentSlots = GetSlotsByCategory(categoryIndex);
        return currentSlots[itemIndex].Item;
    }

    //public ItemBase UseItem(int itemIndex, Pokemon selectedPokemon, int selectedCategory)
    //{

    //    var item = GetItem(itemIndex, selectedCategory);
    //    return UseItem(item, selectedPokemon);
    //}

    //public ItemBase UseItem(ItemBase item, Pokemon selectedPokemon)
    //{

    //    bool itemUsed = item.Use(selectedPokemon);
    //    if (itemUsed)
    //    {
    //        if (!item.IsReuseable)
    //            RemoveItem(item);

    //        return item;
    //    }

    //    return null;
    //}

    public void AddItem(ItemBase item, int count = 1)
    {
        int category = (int)GetCategoryFromItem(item);
        var currentSlots = GetSlotsByCategory(category);

        var itemSlot = currentSlots.FirstOrDefault(slot => slot.Item == item);
        if (itemSlot != null)
        {
            itemSlot.Count += count;
        }
        else
        {
            currentSlots.Add(new ItemsSlot()
            {
                Item = item,
                Count = count
            });
        }

        OnUpdated?.Invoke(); //nullではないときAwakeと同じ処理を実行
    }

    public int GetItemCount(ItemBase item)
    {
        int category = (int)GetCategoryFromItem(item);
        var currentSlots = GetSlotsByCategory(category);

        var itemSlot = currentSlots.FirstOrDefault(slot => slot.Item == item);

        if (itemSlot != null)
            return itemSlot.Count;
        else
            return 0;
    }

    public void RemoveItem(ItemBase item, int ccountToRemove = 1)
    {
        int category = (int)GetCategoryFromItem(item);
        var currentSlots = GetSlotsByCategory(category);

        var itemSlot = currentSlots.First(slot => slot.Item == item);
        itemSlot.Count -= ccountToRemove;
        if (itemSlot.Count == 0)
            currentSlots.Remove(itemSlot);

        OnUpdated?.Invoke();
    }

    public bool HasItem(ItemBase item)
    {
        int category = (int)GetCategoryFromItem(item);
        var currentSlots = GetSlotsByCategory(category);

        return currentSlots.Exists(slot => slot.Item == item);
    }

    ItemsCategory GetCategoryFromItem(ItemBase item)
    {
        if (item is Weapons)
            return ItemsCategory.Weapon;
        else if (item is Armors)
            return ItemsCategory.Armor;
        else if (item is Accessories)
            return ItemsCategory.Accessorie;
        else
            return ItemsCategory.Potion;
    }

    public static CharacterInventory GetInventory()
    {
        return FindObjectOfType<PlayerController>().GetComponent<CharacterInventory>();
    }


    public bool? Evaluate(string predicate, string[] parameters)//特定のアイテムを所持しているか判定
    {
        switch (predicate)
        {
            //case "HasInventoryItem":
                //return HasItem(InventoryItemTV.GetFromID(parameters[0]));
        }

        return null;
    }
}

[Serializable]

public class ItemsSlot
{
    [SerializeField] ItemBase item;
    [SerializeField] int count;

    public ItemsSlot()
    {

    }

    public ItemsSlot(ItemsSaveData saveData)
    {
        item = ItemDB.GetObjectByName(saveData.name);
        count = saveData.count;
    }

    public ItemsSaveData GetSaveData()
    {
        var saveData = new ItemsSaveData()
        {
            name = item.name,
            count = count
        };

        return saveData;
    }

    public ItemBase Item
    {
        get => item;
        set => item = value;
    }
    public int Count
    {
        get => count;
        set => count = value;
    }
}

[Serializable]
public class ItemsSaveData
{
    public string name;
    public int count;
}

[Serializable]
public class InventorysSaveData
{
    public List<ItemsSaveData> weapon;
    public List<ItemsSaveData> armor;
    public List<ItemsSaveData> potion;
}
