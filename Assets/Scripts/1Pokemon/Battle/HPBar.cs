﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] GameObject health;

    public bool IsUpdating { get; private set; }

    public void SetHP(float hpNormalized)
    {
        health.transform.localScale = new Vector3(hpNormalized, 1f);
    }

    public IEnumerator SetHPSmooth(float newHP)
    {
        IsUpdating = true;
        
        float curHP = health.transform.localScale.x;
        float changeAnt = curHP - newHP;

        while(curHP - newHP > Mathf.Epsilon)
        {
            curHP -= changeAnt * Time.deltaTime;
            health.transform.localScale = new Vector3(curHP, 1f);
            yield return null;
        }

        health.transform.localScale = new Vector3(newHP, 1f);

        IsUpdating = false;
    }
}
