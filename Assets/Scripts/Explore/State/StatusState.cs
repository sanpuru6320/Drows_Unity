using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilitys.StateMachine;

public class StatusState : State<GameManager>
{
    [SerializeField] GameObject statusScreen;
    //[SerializeField] List<EquipSlots> eqipButtons;

    public static StatusState i { get; private set; } 

    private void Awake()
    {
        i = this;
    }

    GameManager em;

    public override void Enter(GameManager owner)
    {
        em = owner;
        statusScreen.SetActive(true);
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
        statusScreen.SetActive(false);
    }

}
