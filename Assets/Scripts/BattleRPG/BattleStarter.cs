using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStarter : MonoBehaviour{

    public BattleType[] potentialBattles;

    //public bool cannotFlee;

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))//スペースで戦闘開始(デバック用)
        {
            StartCoroutine(StartBattleCo());
        }
    }

    public IEnumerator StartBattleCo()
    {
        //UIFade.instance.FadeToBlack();

        GameManager.instance.WorldCamera.gameObject.SetActive(false);

        int selectedBattle = Random.Range(0, potentialBattles.Length);

        //BattleManager.instance.rewardItems = potentialBattles[selectedBattle].rewardItems;
        //BattleManager.instance.rewardXP = potentialBattles[selectedBattle].rewardXP;

        yield return new WaitForSeconds(1.5f);

        BattleManager.instance.BattleStart(potentialBattles[selectedBattle].enemies);//, cannotFlee);

        //UIFade.instance.FadeFromBlack();
    }


}
