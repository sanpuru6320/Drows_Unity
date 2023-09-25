using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilitys.StateMachine;

public class ExploreState : State<GameManager>
{
    public GameObject exploreScreen;
    [SerializeField] Button inventoryButton;
    [SerializeField] Button statusButton;
    public static ExploreState i { get; private set; }

    private void Awake()
    {
        i = this;
    }

    private void Start()
    {
        inventoryButton.onClick.AddListener(OpenInventory);
        statusButton.onClick.AddListener(OpenStatus);
    }

    GameManager em;

    public override void Enter(GameManager owner)
    {
        exploreScreen.SetActive(true);
        em = owner;
    }
    public override void Excute()
    {
           
    }

    public override void Exit()
    {
        exploreScreen.SetActive(false);
    }

    void OpenInventory()
    {
        em.StateMachine.Push(InventoryState.i);

    }

    void OpenStatus()
    {
        em.StateMachine.Push(StatusState.i);
    }

}
