//using GameDevTV.Saving;
//using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class CharStats : MonoBehaviour
    {

        public string charName;
        //public float currentEXP;

        public float currentHP, currentSP, strength, defence, wpnPwr, armrPwr, speed;
        public float maxHP = 100;
        public float maxPP = 30;
        public ItemBase equippedWpn = null, equippedArmr = null;
        public Sprite charIamge;

        BaseStats baseStats;

        private void Awake()
        {
            baseStats = GetComponent<BaseStats>();
        }

        void Start()
        {

            StatsUpdate();

        }

        private void StatsUpdate()
        {
            maxHP = GetComponent<BaseStats>().GetStat(Stat.Health);
            maxPP = GetComponent<BaseStats>().GetStat(Stat.PowerPoint);
            strength = GetComponent<BaseStats>().GetStat(Stat.Damage);
            defence = GetComponent<BaseStats>().GetStat(Stat.Defence);
            speed = GetComponent<BaseStats>().GetStat(Stat.Speed);
            currentHP = maxHP;
            currentSP = maxPP;

        }

    }
}
