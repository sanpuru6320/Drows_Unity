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

        }//�P�O���R�O���ɃC�x���g�J�n
            //StartCoroutine(MoveCheck());
    }

    //private IEnumerator MoveCheck() 
    //{
    //    yield break;
    //}

    //�ꎞ�Ԗ��ɃC�x���g���N�����ꍇ
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
