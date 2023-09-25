using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilitys.StateMachine;

public class FreeRoamState : State<GameManager>
{
    public static FreeRoamState i { get; private set; }

    private void Awake()
    {
        i = this;
    }

    GameManager gc;

    public override void Enter(GameManager owner)
    {
        gc = owner;
    }
    public override void Excute()
    {
        PlayerController.i.HandleUpdate();

        //if (Input.GetKeyDown(KeyCode.Return))
        //    gc.StateMachine.Push(GameMenuState.i);
    }
}