using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCheck : MonoBehaviour
{
    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += TimeCheck;
    }

    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= TimeCheck;
    }

    private void TimeCheck()
    {
        if (TimeManager.Hour == 10 && TimeManager.Minute == 30) 
        {

        }//１０時３０分にイベント開始
            //StartCoroutine(MoveCheck());
    }

    //private IEnumerator MoveCheck() 
    //{
    //    yield break;
    //}

    //一時間毎にイベントを起こす場合
    //private void OnEnable()
    //{
    //    TimeManager.OnHourChanged += TimeCheck;
    //}

    //private void OnDisable()
    //{
    //    TimeManager.OnHourChanged -= TimeCheck;
    //}

    //private void TimeCheck()
    //{
    //    StartCoroutine(MoveCheck());
    //}
}
