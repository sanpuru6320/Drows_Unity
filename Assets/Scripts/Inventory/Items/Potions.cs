using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potions", menuName = "Items/Create new potion")]
public class Potions : ItemBase
{
    [Header("Item Details")]
    public int amountToChange;
    public bool affectHP, affectPP, affectStr;

    public override void UseItem(int charToUseOn, ItemBase itemBase)
    {
        CharStats selectedChar = GameManager.instance.playerStats[charToUseOn];

        if (affectHP)
        {
            selectedChar.currentHP += amountToChange;

            if (selectedChar.currentHP > selectedChar.maxHP)
            {
                selectedChar.currentHP = selectedChar.maxHP;
            }
        }

        if (affectPP)
        {
            selectedChar.currentSP += amountToChange;

            if (selectedChar.currentSP > selectedChar.maxPP)
            {
                selectedChar.currentSP = selectedChar.maxPP;
            }
        }

        if (affectStr)
        {
            selectedChar.strength += amountToChange;
        }

      //  GameManager.instance.RemoveItem(nameItem);
    }
}
