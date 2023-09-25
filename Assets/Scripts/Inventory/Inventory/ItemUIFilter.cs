using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIFilter : MonoBehaviour
{
    [SerializeField] ItemsCategory category = ItemsCategory.Weapon;
    [SerializeField] CharacterInventoryUI CharacterInventoryUI;

    Button button;

    public event Action OnCategoryUpdated;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(GetSelectFilter);
    }

    private void GetSelectFilter()
    {
        CharacterInventoryUI.selectedCategory = (int)category;
        OnCategoryUpdated.Invoke();
    }
}
