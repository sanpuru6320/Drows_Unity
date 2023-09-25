using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RowCharacterUI : MonoBehaviour//行動順に応じてキャラクターアイコンをセット
{
    [SerializeField]
    Image iconField;

    public int objectIndex;
    
    public Image characterImage;

    private void Update()
    {
        objectIndex = transform.GetSiblingIndex();

    }
}
