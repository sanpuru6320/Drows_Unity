using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] int loadSceneIndex;
    public void LoadScene()
    {
        SceneManager.LoadSceneAsync(loadSceneIndex);
    }
}
