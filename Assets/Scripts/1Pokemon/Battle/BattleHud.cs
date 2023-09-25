//using DG.Tweening;
using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] Text statusText;
    [SerializeField] HPBar hPBar;
    [SerializeField] HPBar skillBar;
    [SerializeField] GameObject expBar;

    [SerializeField] Color psnColor;
    [SerializeField] Color brnColor;
    [SerializeField] Color slpColor;
    [SerializeField] Color parColor;
    [SerializeField] Color frzColor;

    Pokemon _pokemon;
    Dictionary<ConditionID, Color> statusColors;
    BattleChar battleChar;
    CharStats charStats;

    private void OnEnable()
    {
        battleChar = GetComponentInParent<BattleChar>();
        charStats = FindAnyObjectByType<CharStats>();
    }

    private void Update()
    {
        if(battleChar != null)
        {
            //Debug.Log((float)battleChar.currentHP);
            //Debug.Log((float)battleChar.maxHP);

            hPBar.SetHP((float)battleChar.currentHP / battleChar.maxHP);
            skillBar.SetHP((float)battleChar.currentPP / battleChar.maxPP);
            //UpdateHP();
            //UpdateMP();
        }
        
    }

    //public void SetData(Pokemon pokemon)
    //{
    //    if(_pokemon != null)
    //    {
    //        _pokemon.OnHPChanged -= UpdateHP;
    //        _pokemon.OnStatusChanged -= SetstatusText;
    //    }
        
    //    _pokemon = pokemon;

    //    nameText.text = pokemon.Base.Name;
    //    SetLevel();
    //    levelText.text = "Lvl" + pokemon.Level;
    //    hPBar.SetHP((float)battleChar.currentHP / battleChar.maxHP);
    //    SetExp();
        

    //    statusColors = new Dictionary<ConditionID, Color>() 
    //    { 
    //        { ConditionID.psn, psnColor }, 
    //        { ConditionID.brn, brnColor }, 
    //        { ConditionID.slp, slpColor }, 
    //        { ConditionID.par, parColor }, 
    //        { ConditionID.frz, frzColor }, 
    //    };

    //    SetstatusText();
    //    _pokemon.OnStatusChanged += SetstatusText;
    //    _pokemon.OnHPChanged += UpdateHP;
    //}

    //void SetstatusText()
    //{
    //    if (_pokemon.Status == null)
    //    {
    //        statusText.text = ""; 
    //    }
    //    else
    //    {
    //        statusText.text = _pokemon.Status.Id.ToString().ToUpper();
    //        statusText.color = statusColors[_pokemon.Status.Id];
    //    }
    //}

    //public void SetLevel()
    //{
    //    levelText.text = "Lvl" + _pokemon.Level;
    //}

    //public void SetExp()
    //{
    //    if (expBar == null) return;

    //    float normalizedExp = GetNormalizedExp();
    //    expBar.transform.localScale = new Vector3(normalizedExp, 1, 1); 
    //}

    //public IEnumerator SetExpSmooth(bool reset = false)
    //{
    //    if (expBar == null) yield break;

    //    if (reset)
    //        expBar.transform.localScale = new Vector3(0, 1, 1);

    //    float normalizedExp = GetNormalizedExp();
    //    //yield return expBar.transform.DOScaleX(normalizedExp, 1.5f).WaitForCompletion();
    //}
        
    //float GetNormalizedExp()
    //{
    //    int currLevelExp = _pokemon.Base.GetExpForLevel(_pokemon.Level);
    //    int nextLevelExp = _pokemon.Base.GetExpForLevel(_pokemon.Level + 1);

    //    float normalizedExp = (float)(_pokemon.Exp - currLevelExp) / (nextLevelExp - currLevelExp);
    //    return Mathf.Clamp01(normalizedExp);
    //}

    public void UpdateHP()
    {
        StartCoroutine(UpdateHPAsync());
    }

    public IEnumerator UpdateHPAsync()
    {
        //if (battleChar.isPlayer)
        //{
        //    yield return hPBar.SetHPSmooth((float)charStats.currentHP / charStats.maxHP);
        //}
        yield return hPBar.SetHPSmooth((float)battleChar.currentHP / battleChar.maxHP); 
    }

    public void UpdateMP()
    {
        StartCoroutine(UpdatePPAsync());
    }

    public IEnumerator UpdatePPAsync()
    {
        //if (battleChar.isPlayer)
        //{
        //    yield return hPBar.SetHPSmooth((float)charStats.currentMP / charStats.maxMP);
        //}
        yield return hPBar.SetHPSmooth((float)battleChar.currentPP / battleChar.maxPP);
    }

    public IEnumerator WaitForHPUpdate()
    {
        yield return new WaitUntil(() => hPBar.IsUpdating == false);
    }

    public void ClearData()
    {
        if (_pokemon != null)
        {
            _pokemon.OnHPChanged -= UpdateHP;
            //_pokemon.OnStatusChanged -= SetstatusText;
        }
    }
}
