using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RowCharacterUI : MonoBehaviour//�s�����ɉ����ăL�����N�^�[�A�C�R�����Z�b�g
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
