using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTexts;
    [SerializeField] TextMeshProUGUI countTexts;

    RectTransform rectTransform;

    public TextMeshProUGUI NameTexts => nameTexts;
    public TextMeshProUGUI CountTexts => countTexts;

    public float Height => rectTransform.rect.height;

    public void SetItemData(ItemsSlot itemsSlot)
    {
        rectTransform = GetComponent<RectTransform>();
        Debug.Log(itemsSlot.Item.Name);
        nameTexts.text = itemsSlot.Item.Name;
        //countTexts.text = $"X {itemsSlot.Count}";
    }

    //public void SetNameAndPrice(ItemBase item)
    //{
    //    rectTransform = GetComponent<RectTransform>(); 
    //    nameText.text = item.Name;
    //    countText.text = $"X {item.Price}";
    //}
}

public class ItemSlot
{
    [SerializeField] ItemBase item;
    [SerializeField] int count;

    public ItemSlot()
    {

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
