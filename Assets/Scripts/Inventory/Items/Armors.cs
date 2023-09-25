using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armors", menuName = "Items/Create new armor")]
public class Armors : ItemBase
{
    [Header("Armor Details")]

    public int armorStrength;

    public override void UseItem(int charToUseOn, ItemBase itemBase)
    {
        CharStats selectedChar = GameManager.instance.playerStats[charToUseOn];

        //if (selectedChar.equippedArmr != "")
        //{
        //    GameManager.instance.AddItem(selectedChar.equippedArmr);
        //}

        selectedChar.equippedArmr = itemBase;
        selectedChar.armrPwr = armorStrength;

       // GameManager.instance.RemoveItem(nameItem);
    }

}
