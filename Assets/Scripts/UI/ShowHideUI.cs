using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowHideUI : MonoBehaviour
{
    [SerializeField] GameObject selectedScreen;
    [SerializeField] int loadSceneIndex;

    public void HideScreen()
    {
        selectedScreen.SetActive(false);
    }

    public void ShowScreen()
    {
        selectedScreen.SetActive(true);
    }

    public void Quit()
    {
        SceneManager.LoadSceneAsync(loadSceneIndex);
    }
}
