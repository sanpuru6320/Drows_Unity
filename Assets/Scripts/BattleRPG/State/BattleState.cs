using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utilitys.StateMachine;

public class BattleState : State<GameManager>
{
    public static BattleState i { get; private set; }

    BattleStarter battleStarter;

    private void Awake()
    {
        i = this;
    }

    private void Start()
    {
        battleStarter = FindObjectOfType<BattleStarter>();
    }

    GameManager gc;

    public override void Enter(GameManager owner)
    {
        gc = owner;


        var prevState = gc.StateMachine.GetPrevState();
        if (prevState == ExploreState.i)
        {
            ExploreState.i.exploreScreen.SetActive(false);
        }
        StartCoroutine(battleStarter.StartBattleCo());
    }

    public override void Excute()
    {
        
    }

    public override void Exit()
    {
        if (ExploreState.i.exploreScreen)
        {
            ExploreState.i.exploreScreen.SetActive(true);
        }
        
    }
}
