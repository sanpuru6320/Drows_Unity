using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleChar : MonoBehaviour {

    public bool isPlayer;
    public string[] movesAvailable;//使用可能スキル

    public string charName;
    public float currentHP,currentPP,strength, defence, wpnPower, armrPower;
    public float maxHP = 100;
    public float maxPP = 50;
    public float speed;
    //public float rewardXP;
    public bool onAction = true;

    public SpriteRenderer theSprite;
    public Sprite aliveSprite,deadSprite; //生存時、行動不能時のスプライト(プレイヤーで使用)

    private bool shouldFade;
    public float fadeSpeed = 1f;

	// Use this for initialization
	void Start () {

        if (!isPlayer)//敵のステータス設定
        {
            maxHP = GetComponent<BaseStats>().GetStat(Stat.Health);
            maxPP = GetComponent<BaseStats>().GetStat(Stat.PowerPoint);
            strength = GetComponent<BaseStats>().GetStat(Stat.Damage);
            defence = GetComponent<BaseStats>().GetStat(Stat.Defence);
            speed = GetComponent<BaseStats>().GetStat(Stat.Speed);
            currentHP = maxHP;
            currentPP = maxPP;

            //rewardXP = GetComponent<BaseStats>().GetStat(Stat.ExperienceReward);
        }

    }
	
	void Update () {
		if(shouldFade)//倒された敵のフェードアウト
        {
            theSprite.color = new Color(Mathf.MoveTowards(theSprite.color.r, 1f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theSprite.color.g, 0f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theSprite.color.b, 0f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theSprite.color.a, 0f, fadeSpeed * Time.deltaTime));
            if(theSprite.color.a == 0)
            {
                gameObject.SetActive(false);
            }
        }
	}

    public void EnemyFade()
    {
        shouldFade = true;
    }
}
