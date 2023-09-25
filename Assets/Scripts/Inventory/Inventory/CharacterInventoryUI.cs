//using GDE.GenericSelectionUI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using RPG.Shops;

public class CharacterInventoryUI : MonoBehaviour//SelectionUI<TextSlot>
{
    [SerializeField] GameObject itemsList;
    [SerializeField] ItemSlotUI itemsSlotUI;
    [SerializeField] Button itemsButton;

    [SerializeField] TextMeshProUGUI itemsText;
    [SerializeField] Image itemsIcon;
    [SerializeField] TextMeshProUGUI ItemsDescription;

    [SerializeField] int inventorySize = 16;

    public Text useButtonText;

    const int itemsViewport = 8;

    public int selectedCategory = 0;

    [SerializeField] List<ItemSlotUI> slotUIList;
    CharacterInventory itemsInventory;
    //ShopTV shopTV;
    //ItemsMerchant itemsMercant;
    //RectTransform itemsListRect;

    public List<ItemUIFilter> itemUIFilter;

    int selectedItems = 0;

    private void Awake()
    {
        itemsInventory = CharacterInventory.GetInventory();
        //itemsListRect = itemsList.GetComponent<RectTransform>();
    }

    private void Start()
    {

        UpdateItemsList();

        itemsInventory.OnUpdated += UpdateItemsList;
        //shopTV.onUpdated += UpdateItemsList;
        foreach (var itemUI in itemUIFilter)
        {
            itemUI.OnCategoryUpdated += HandleUpdete;
        }
    }
    void UpdateItemsList()
    {
        // Clear all the existing itemss
        foreach (Transform child in itemsList.transform)
            Destroy(child.gameObject);

        slotUIList = new List<ItemSlotUI>();
        foreach (var itemSlot in itemsInventory.GetSlotsByCategory(selectedCategory))
        {
            var slotUIObj = Instantiate(itemsSlotUI, itemsList.transform);
            slotUIObj.SetItemData(itemSlot);

            slotUIObj.GetComponent<Button>().onClick.AddListener(() => ChangeItemsUI(itemSlot));

            slotUIList.Add(slotUIObj);

        }

        Debug.Log(slotUIList.Count);
        Debug.Log(inventorySize - slotUIList.Count);


        if (slotUIList.Count <= inventorySize)//‹ó‚Ì˜g’Ç‰Á
        {
            int emptyLength = slotUIList.Count;
            for (int i = 0; i < inventorySize - emptyLength; i++)
            {
                var slotUIEmptyObj = Instantiate(itemsSlotUI, itemsList.transform);
                slotUIList.Add(slotUIEmptyObj);
                Debug.Log(i);
            }
        }
        

        //SetItems(slotUIList.Select(s => s.GetComponent<TextSlot>()).ToList());

        //UpdateSelectionUI();


    }


    //public void UpdateSelectionUI()
    //{
    //    //base.UpdateSelectionUI();

    //    var slots = itemsInventory.GetSlotsByCategory(selectedCategory);

    //    if (slots.Count > 0)
    //    {
    //        var items = slots[selectedItem].Item;
    //        itemsText.text = items.Name;
    //        itemsIcon.sprite = items.Icon;
    //        ItemsDescription.text = items.Description;
    //    }

    //}

    public void HandleUpdete()
    {
        ResetSelection();
        //categoryText.text = CharacterInventory.ItemCategories[selectedCategory];
        UpdateItemsList();

        //base.HandleUpdete();
    }

    public void ChangeItemsUI(ItemsSlot itemsSlot)
    {
        itemsText.text = itemsSlot.Item.Name;
        itemsIcon.sprite = itemsSlot.Item.Icon;
        ItemsDescription.text = itemsSlot.Item.Description;


        if (selectedCategory == 0 || selectedCategory == 1)
        {
            useButtonText.text = "Equip";
        }
        else
        {
            useButtonText.text = "Use";
        }

        //GameMenu.instance.activeItems = itemsSlot.Item;

    }

    void ResetSelection()
    {
        selectedItems = 0;

        //upArrow.gameObject.SetActive(false);
        //downArrow.gameObject.SetActive(false);

        itemsIcon.sprite = null;
        ItemsDescription.text = "";
    }

    void ConfiarmItems()
    {
        var currentSlot = itemsInventory.weaponSlots;
        var itemsSlot = currentSlot.First((slot => slot.Item.Name == itemsText.text));

        itemsInventory.weaponSlots.Remove(itemsSlot);
        UpdateItemsList();
    }

    //public ItemBase SelectedItems => itemsInventory.GetItem(selectedItems);

}