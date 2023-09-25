using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlots : MonoBehaviour
{
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenEqipMenu);
    }

    void OpenEqipMenu()
    {
        GameManager.instance.StateMachine.Push((InventoryState.i));
    }
}
