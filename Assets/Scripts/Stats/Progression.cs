using UnityEngine;
using System.Collections.Generic;
using System;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject //Classと各Level時の追加ステータス設定(仮)
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        Dictionary<CharacterClass, Dictionary<Stat, float>> lookupTable = null;

        public float GetStat(Stat stat, CharacterClass characterClass)//, int level)
        {
            BuildLookup();

            if (!lookupTable[characterClass].ContainsKey(stat))
            {
                return 0;
            }

            float levels = lookupTable[characterClass][stat];

            if (levels == 0)
            {
                return 0;
            }

            //if (levels < level)
            //{
            //    return levels;
            //}

            return levels;
        }

        public int GetLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLookup();

            float levels = lookupTable[characterClass][stat];
            return (int)levels;
        }

        private void BuildLookup()
        {
            if (lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float>>();

            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                var statLookupTable = new Dictionary<Stat, float>();

                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.parameter;
                }

                lookupTable[progressionClass.characterClass] = statLookupTable;
            }
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float parameter;
        }
    }
}