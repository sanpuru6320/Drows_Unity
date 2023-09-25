using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilitys.StateMachine;

public class InventoryState : State<GameManager>
{
    [SerializeField] GameObject inventoryScreen;
    public static InventoryState i { get; private set; }

    private void Awake()
    {
        i = this;
    }

    GameManager em;

    public override void Enter(GameManager owner)
    {
        em = owner;
        inventoryScreen.SetActive(true);
    }

    public override void Excute()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            em.StateMachine.Pop();
        }

        //if (Keyboard.current.escapeKey.wasPressedThisFrame)
        //{
        //    em.StateMachine.Pop();
        //}
    }

    public override void Exit()
    {
        inventoryScreen.SetActive(false);
    }
}
