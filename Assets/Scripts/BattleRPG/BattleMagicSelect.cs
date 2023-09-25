using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMagicSelect : MonoBehaviour {

    public string skillName;
    public int skillCost;
    public Text nameText;
    public Text costText;

    public void Press()
    {
        if (BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn].currentPP >= skillCost)
        {
            BattleManager.instance.skillMenu.SetActive(false);
            BattleManager.instance.OpenTargetMenu(skillName);
            BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn].currentPP -= skillCost;
        } else
        {
            //let player know there is not enough PP
            BattleManager.instance.battleNotice.theText.text = "Not Enough PP!";
            BattleManager.instance.battleNotice.Activate();
            BattleManager.instance.skillMenu.SetActive(false);
        }
    }
}
