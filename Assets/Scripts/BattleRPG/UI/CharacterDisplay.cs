using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour //�s�����̃v���C���[�\��
{
    [SerializeField] Image Image;
    private void Update()
    {
        if (BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn].isPlayer)
        {
            Image.sprite = BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn].aliveSprite;
        }
    }
}
