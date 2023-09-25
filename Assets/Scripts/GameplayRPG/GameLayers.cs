using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] LayerMask solidobjectsLayer;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] LayerMask playerLayer;

   
    public static GameLayers i { get; set; }

    private void Awake()
    {
        i = this;
    }
    public LayerMask SolidLayer { get => solidobjectsLayer; }
    public LayerMask InteractableLayer { get => interactableLayer; }
    public LayerMask PlayerLayer
    {
        get => playerLayer;
    }
}
