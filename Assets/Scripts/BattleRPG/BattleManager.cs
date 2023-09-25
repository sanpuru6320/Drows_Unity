using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RPG.Stats;
using Unity.VisualScripting;
using System;
using Random = UnityEngine.Random;
using TMPro;

public class BattleManager : MonoBehaviour {

    public static BattleManager instance;

    private bool battleActive;

    public GameObject battleScene;

    public CharStats[] playerStats;

    public Transform[] playerPositions;
    public Transform[] enemyPositions;

    public BattleChar[] playerPrefabs;
    public BattleChar[] enemyPrefabs;

    public List<BattleChar> activeBattlers = new List<BattleChar>();//戦闘中のキャラ

    public int currentTurn = 0;
    public bool turnWaiting; //敵の行動中false

    public GameObject uiButtonsHolder;

    public BattleMove[] movesList; //使用できるスキルリスト
    //public GameObject enemyAttackEffect;

    public DamageNumber theDamageNumber; //ダメージ表示

    //public Text[] playerName, playerHP, playerPP;

    public GameObject targetMenu;
    public BattleTargetButton[] targetButtons;

    public GameObject skillMenu;
    public BattleMagicSelect[] skillButtons;

    public BattleNotification battleNotice;

    
    private bool fleeing;

    public string gameOverScene;//シーン名を入力

    public float[][] speeds;

    //protected float rewardXP;
    //public string[] rewardItems;
    //public int chanceToFlee = 35;
    //public bool cannotFlee;

    private bool isStart = false;

    [SerializeField] OrderOfActionUI orderOfActionUI;

    [SerializeField] TMP_Text dialogText;

    [SerializeField] Image characterIcon;

    void Start () {
        instance = this;
        //DontDestroyOnLoad(gameObject);
	}
	
	void Update () {

        if(battleActive)
        {
            if(turnWaiting)
            {
                
                if (activeBattlers[currentTurn].isPlayer)//Playerターン
                {
                    
                    uiButtonsHolder.SetActive(true);
                } else                                  //Enemyターン
                {
                    uiButtonsHolder.SetActive(false);

                    //enemy should attack
                    StartCoroutine(EnemyMoveCo());
                }
            }

            //if(Input.GetKeyDown(KeyCode.N))
            //{
            //    NextTurn();
            //}
        }

    }

    public void BattleStart(string[] enemiesToSpawn) //, bool setCannotFlee)
    {
        if(!battleActive)
        {
            //cannotFlee = setCannotFlee;

            battleActive = true;
            battleScene.SetActive(true);

            //AudioSetManager.instance.PlayBGM(0);

            for(int i = 0; i < playerStats.Length; i++)//プレイヤー配置
            {
                if(playerStats[i].gameObject.activeInHierarchy)
                {
                    for(int j = 0; j < playerPrefabs.Length; j++)
                    {
                        if(playerPrefabs[j].charName == playerStats[i].charName) //設定しているキャラクター名と一致しているか判定
                        {
                            BattleChar newPlayer = Instantiate(playerPrefabs[j], playerPositions[i].position, playerPositions[i].rotation);
                            newPlayer.transform.parent = playerPositions[i];
                            activeBattlers.Add(newPlayer);


                            CharStats thePlayer = playerStats[i]; //CharStatsからステータス参照
                            activeBattlers[i].currentHP = thePlayer.currentHP;
                            activeBattlers[i].maxHP = thePlayer.maxHP;
                            activeBattlers[i].currentPP = thePlayer.currentSP;
                            activeBattlers[i].maxPP = thePlayer.maxPP;
                            activeBattlers[i].strength = thePlayer.strength;
                            activeBattlers[i].defence = thePlayer.defence;
                            activeBattlers[i].wpnPower = thePlayer.wpnPwr;
                            activeBattlers[i].armrPower = thePlayer.armrPwr;
                            activeBattlers[i].speed = thePlayer.speed;
                        }
                    }


                }
            }

            for (int i = 0; i < enemiesToSpawn.Length; i++)//敵配置
            {
                if (enemiesToSpawn[i] != "")
                {
                    for (int j = 0; j < enemyPrefabs.Length; j++)
                    {
                        if (enemyPrefabs[j].charName == enemiesToSpawn[i]) //設定しているキャラクター名と一致しているか判定
                        {
                            BattleChar newEnemy = Instantiate(enemyPrefabs[j], enemyPositions[i].position, enemyPositions[i].rotation);
                            newEnemy.transform.parent = enemyPositions[i];
                            activeBattlers.Add(newEnemy);
                        }
                    }
                }
            }

            Comparison<BattleChar> comparison = (x, y) => x.speed.CompareTo(y.speed);//各キャラのspeedをもとに昇順に並べ、反転
            activeBattlers.Sort(comparison);
            activeBattlers.Reverse();

            

            turnWaiting = true;
            currentTurn = 0;
            foreach (var ac in activeBattlers)
            {
                ac.onAction = true;
            }
            //UpdateUIStats();

        }
    }

    public void NextTurn()
    {
        currentTurn++;
        if (!isStart)
        {
            orderOfActionUI.IndexChange();
        }
        
        if (currentTurn >= activeBattlers.Count)//一周したらspeedをもとに再計算
        {
            Comparison<BattleChar> comparison = (x, y) => x.speed.CompareTo(y.speed);//各キャラのspeedをもとに昇順に並べる

            activeBattlers.Sort(comparison);
            activeBattlers.Reverse();
            currentTurn = 0;

            foreach (var ac in activeBattlers)
            {
                ac.onAction = true;
            }
        }

        turnWaiting = true;

        UpdateBattle();
        //UpdateUIStats();
    }

    public void UpdateBattle()
    {
        bool allEnemiesDead = true;
        bool allPlayersDead = true;

        for(int i = 0; i < activeBattlers.Count; i++)
        {
            if(activeBattlers[i].currentHP < 0)
            {
                activeBattlers[i].currentHP = 0;
            }

            if(activeBattlers[i].currentHP == 0)
            {
                //Handle dead battler
                if(activeBattlers[i].isPlayer)
                {
                    activeBattlers[i].theSprite.sprite = activeBattlers[i].deadSprite;
                } else
                {
                    activeBattlers[i].EnemyFade();
                }

            } else
            {
                if(activeBattlers[i].isPlayer)
                {
                    allPlayersDead = false;
                } else
                {
                    allEnemiesDead = false;
                }
            }
        }

        if(allEnemiesDead || allPlayersDead)
        {
            if(allEnemiesDead)
            {
                //end battle in victory
                StartCoroutine(EndBattleCo());
                GameManager.instance.WorldCamera.gameObject.SetActive(true);
            } else
            {
                //end battle in failure
                StartCoroutine(GameOverCo());
            }

            /* battleScene.SetActive(false);
            GameManager.instance.battleActive = false;
            battleActive = false; */
        } else
        {
            while(activeBattlers[currentTurn].currentHP == 0)
            {

                currentTurn++;                
                if (currentTurn >= activeBattlers.Count)
                {
                    currentTurn = 0;
                }
                //isStart = false;
            }

            if (!isStart)
            {
                isStart = true;
            }
            else
            {
                orderOfActionUI.IndexChange();
            }
            
        }

        if (activeBattlers[currentTurn].isPlayer)//画面左下に行動中のプレイヤーキャラ表示
        {
            characterIcon.sprite = activeBattlers[currentTurn].aliveSprite;
        }



    }

    public IEnumerator EnemyMoveCo()
    {
        turnWaiting = false;
        yield return new WaitForSeconds(1f);
        EnemyAttack();
        yield return new WaitForSeconds(1f);
        activeBattlers[currentTurn].onAction = false;
        NextTurn();
    }

    public void EnemyAttack()
    {
        List<int> players = new List<int>();
        for(int i = 0; i < activeBattlers.Count; i++)
        {
            if(activeBattlers[i].isPlayer && activeBattlers[i].currentHP > 0)
            {
                players.Add(i);
            }
        }
        int selectedTarget = players[Random.Range(0, players.Count)];//攻撃対象をランダムに選択

        int selectAttack = Random.Range(0, activeBattlers[currentTurn].movesAvailable.Length);//スキルをランダムに設定
        int movePower = 0;
        for(int i = 0; i < movesList.Length; i++)
        {
            if(movesList[i].moveName == activeBattlers[currentTurn].movesAvailable[selectAttack])
            {
                Instantiate(movesList[i].theEffect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
                movePower = movesList[i].movePower;
            }
        }
        if (!isStart)//戦闘開始直後の行動順表示
        {
            orderOfActionUI.IndexChange();
        }
        

        //Instantiate(enemyAttackEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);

        DealDamage(selectedTarget, movePower);
    }

    public void DealDamage(int target, int movePower)
    {
        float atkPwr = activeBattlers[currentTurn].strength + activeBattlers[currentTurn].wpnPower; //攻撃力+武器の強さ
        float defPwr = activeBattlers[target].defence + activeBattlers[target].armrPower;//防御力 + 防具の強さ

        float damageCalc = (atkPwr / defPwr) * movePower * Random.Range(.9f, 1.1f);
        int damageToGive = Mathf.RoundToInt(damageCalc);

        Debug.Log(activeBattlers[currentTurn].charName + " is dealing " + damageCalc + "(" + damageToGive + ") damage to " + activeBattlers[target].charName);
        dialogText.text = activeBattlers[currentTurn].charName + " is dealing " + damageToGive + " damage to " + activeBattlers[target].charName; //行動表示

        activeBattlers[target].currentHP -= damageToGive;

        Instantiate(theDamageNumber, activeBattlers[target].transform.position, activeBattlers[target].transform.rotation).SetDamage(damageToGive);//ダメージ表示

        //UpdateUIStats();    
    }

    public void PlayerAttack(string moveName, int selectedTarget)
    {

        int movePower = 0;
        for (int i = 0; i < movesList.Length; i++)//スキルのエフェクト表示(通常攻撃の場合はSlash)
        {
            if (movesList[i].moveName == moveName)
            {
                Instantiate(movesList[i].theEffect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
                movePower = movesList[i].movePower;
            }
        }

        //Instantiate(enemyAttackEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);

        DealDamage(selectedTarget, movePower);

        uiButtonsHolder.SetActive(false);
        targetMenu.SetActive(false);
        activeBattlers[currentTurn].onAction = false;

        NextTurn();

    }

    public void OpenTargetMenu(string moveName)//敵の選択メニュー
    {
        targetMenu.SetActive(true);

        List<int> Enemies = new List<int>();
        for(int i = 0; i < activeBattlers.Count; i++)
        {
            if(!activeBattlers[i].isPlayer)
            {
                Enemies.Add(i);
            }
        }

        for(int i = 0; i < targetButtons.Length; i++)
        {
            if(Enemies.Count > i && activeBattlers[Enemies[i]].currentHP > 0)
            {
                targetButtons[i].gameObject.SetActive(true);

                targetButtons[i].moveName = moveName;
                targetButtons[i].activeBattlerTarget = Enemies[i];
                targetButtons[i].targetName.text = activeBattlers[Enemies[i]].charName;
            } else
            {
                targetButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void OpenMagicMenu()//スキルメニュー表示
    {
        skillMenu.SetActive(true);

        for(int i = 0; i < skillButtons.Length; i++)
        {
            if(activeBattlers[currentTurn].movesAvailable.Length > i)
            {
                skillButtons[i].gameObject.SetActive(true);

                skillButtons[i].skillName = activeBattlers[currentTurn].movesAvailable[i];
                skillButtons[i].nameText.text = skillButtons[i].skillName;

                for(int j = 0; j < movesList.Length; j++)
                {
                    if(movesList[j].moveName == skillButtons[i].skillName)
                    {
                        skillButtons[i].skillCost = movesList[j].moveCost;
                        skillButtons[i].costText.text = skillButtons[i].skillCost.ToString();
                    }
                }

            } else
            {
                skillButtons[i].gameObject.SetActive(false);
            }
        }
    }



    public IEnumerator EndBattleCo()//戦利品、BGM、フェードアウトは未実装
    {
        
        battleActive = false;
        uiButtonsHolder.SetActive(false);
        targetMenu.SetActive(false);
        skillMenu.SetActive(false);


        yield return new WaitForSeconds(.5f);

        //UIFade.instance.FadeToBlack();

        yield return new WaitForSeconds(1.5f);

        for(int i = 0; i < activeBattlers.Count; i++)//戦闘後のステータス反映
        {
            if(activeBattlers[i].isPlayer)
            {
                for(int j = 0; j < playerStats.Length; j++)
                {
                    if(activeBattlers[i].charName == playerStats[j].charName)
                    {
                        playerStats[j].currentHP = activeBattlers[i].currentHP;
                        playerStats[j].currentSP = activeBattlers[i].currentPP;
                    }
                }
            }
            else
            {
                //rewardXP += activeBattlers[i].rewardXP;
            }

            Destroy(activeBattlers[i].gameObject);
        }

        //UIFade.instance.FadeFromBlack();
        battleScene.SetActive(false);
        activeBattlers.Clear();
        currentTurn = 0;

        GameManager.instance.StateMachine.Pop();
        //GameManager.instance.battleActive = false;
        if(fleeing)
        {
            //GameManager.instance.battleActive = false;
            fleeing = false;
        } else
        {
            //BattleReward.instance.OpenRewardScreen(rewardXP, rewardItems);
        }

        //rewardXP = 0;
        //AudioSetManager.instance.PlayBGM(FindObjectOfType<CameraController>().musicToPlay);
    }

    public IEnumerator GameOverCo()
    {
        battleActive = false;
        //UIFade.instance.FadeToBlack();
        yield return new WaitForSeconds(1.5f);
        battleScene.SetActive(false);
        SceneManager.LoadScene(gameOverScene);
    }
}







//public void Flee()//未実装
//{
//    if (cannotFlee)
//    {
//        battleNotice.theText.text = "Can not flee this battle!";
//        battleNotice.Activate();
//    }
//    else
//    {
//        int fleeSuccess = Random.Range(0, 100);
//        if (fleeSuccess < chanceToFlee)
//        {
//            //end the battle
//            //battleActive = false;
//            //battleScene.SetActive(false);
//            fleeing = true;
//            StartCoroutine(EndBattleCo());
//        }
//        else
//        {
//            NextTurn();
//            battleNotice.theText.text = "Couldn't escape!";
//            battleNotice.Activate();
//        }
//    }

//}

//public void UpdateUIStats()//今の所未使用
//{

//    for (int i = 0; i < playerName.Length; i++)
//    {
//        if (activeBattlers.Count > i)
//        {
//            if (activeBattlers[i].isPlayer)
//            {
//                BattleChar playerData = activeBattlers[i];

//                playerName[i].gameObject.SetActive(true);
//                playerName[i].text = playerData.charName;
//                playerHP[i].text = Mathf.Clamp(playerData.currentHP, 0, int.MaxValue) + "/" + playerData.maxHP;
//                playerPP[i].text = Mathf.Clamp(playerData.currentPP, 0, int.MaxValue) + "/" + playerData.maxPP;

//            }
//            else
//            {
//                playerName[i].gameObject.SetActive(false);
//            }
//        }
//        else
//        {
//            playerName[i].gameObject.SetActive(false);
//        }
//    }
//}

