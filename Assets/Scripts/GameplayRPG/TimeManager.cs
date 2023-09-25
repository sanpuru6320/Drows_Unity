using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    private float minuteToRealTime = 30f;//1•ª
    private float timer;
    bool firstInvoke = false;

    void Start()
    {
        Minute = 0;
        Hour = 10;
        timer = minuteToRealTime;

    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Minute++;
            OnHourChanged?.Invoke();

            if (Minute >= 60)
            {
                Hour++;
                Minute = 0;
                OnHourChanged?.Invoke();
            }

            timer = minuteToRealTime;
        }
    }
}
