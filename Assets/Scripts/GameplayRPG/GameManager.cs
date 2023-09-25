using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilitys.StateMachine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public StateMachine<GameManager> StateMachine { get; private set; }

    [SerializeField] Camera worldCamera;


    [SerializeField] PlayerController playerController;

    public CharStats[] playerStats;

    public bool gameMenuOpen, dialogActive, fadingBetweenAreas, shopActive, battleActive;

    public int currentGold;

	// Use this for initialization
	void Start () {
        instance = this;

        //DontDestroyOnLoad(gameObject);

        StateMachine = new StateMachine<GameManager>(this);

        if(SceneManager.GetActiveScene().name == "ExploreScene2")
        {
            StateMachine.ChangeState(ExploreState.i);
        }
        else
        {
            StateMachine.ChangeState(FreeRoamState.i);
        }
        
    } 

    // Update is called once per frame
    void Update()
    {

        StateMachine.Excute();
    }


    private void OnGUI()
    {
        var style = new GUIStyle();
        style.fontSize = 24;

        GUILayout.Label("STATE STACK", style);
        foreach (var state in StateMachine.StateStack)
        {
            GUILayout.Label(state.GetType().ToString(), style);
        }
    }

    public PlayerController PlayerController => playerController;

    public Camera WorldCamera => worldCamera;

}
