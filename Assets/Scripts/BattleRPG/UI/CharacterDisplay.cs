using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour //行動中のプレイヤー表示
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
