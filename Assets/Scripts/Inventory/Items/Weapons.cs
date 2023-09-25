using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "Items/Create new weapon")]
public class Weapons : ItemBase
{
    [Header("Weapon Details")]
    public int weaponStrength;

    public  override void UseItem(int charToUseOn, ItemBase itemBase)
    {
        CharStats selectedChar = GameManager.instance.playerStats[charToUseOn];

        //if (selectedChar.equippedWpn != "")
        //{
        //    GameManager.instance.AddItem(selectedChar.equippedWpn);
        //}

        selectedChar.equippedWpn = itemBase;
        selectedChar.wpnPwr = weaponStrength;

       // GameManager.instance.RemoveItem(nameItem);


    }
}
