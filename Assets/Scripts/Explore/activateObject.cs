using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateObject : MonoBehaviour, Interactable
{
    [SerializeField] GameObject closedObject;
    [SerializeField] GameObject openObject;
    [SerializeField] bool isOpen = false;
    public IEnumerator Interact(Transform initiator)
    {
        if (!isOpen)
        {
            closedObject.SetActive(false);
            openObject.SetActive(true);
            isOpen = true;
            yield break;
        }
    }

}
