using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseScreen : MonoBehaviour
{
    [SerializeField] GameObject activeScreen;

    private void Update()
    {
        if (Input.GetKeyDown("Escape"))
            ClosedScreen();
    }

    public void ClosedScreen()
    {
        activeScreen.SetActive(false);
    }
}
