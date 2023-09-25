using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    public GameObject button1, button2, button3;

    public void FirstButtonUpdate()
    {
        //clear selected button
        EventSystem.current.SetSelectedGameObject(null);
        //set new button
        EventSystem.current.SetSelectedGameObject(button2);
    } 
}
