using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleEncounter : MonoBehaviour
{
    public void StartBattle() //Load Battle Scene
    {
        SceneManager.LoadSceneAsync("ClearScene");
    }
}
