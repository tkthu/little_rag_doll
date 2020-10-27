using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Text timerMinutes;
    public Text timerSeconds;
    public Text timerSeconds100;

    private float startTime;
    private float stopTime;
    private float timerTime;
    private bool isRunning = false;


    public float getCurrentStopTime()// ?????????
    {
        return stopTime;
    }

    public float getCurrentTime()
    {
        return timerTime;
    }

    public void TimerStart(float startAt = 0)
    {
        if(!isRunning)
        {
            Time.timeScale = 1f;
            isRunning = true;
            stopTime = startAt;
            startTime = Time.time;
        }
    }

    public float TimerStop()
    {
        if (isRunning)
        {
            Time.timeScale = 0f;
            isRunning = false;
            stopTime = timerTime;
        }
        return stopTime;
    }

    /*public void TimerReset()
    {
        timerMinutes.text = timerSeconds.text = timerSeconds100.text = "00";
        isRunning = false;
        TimerStart();
    }*/

    void Update()
    {
        timerTime = stopTime + (Time.time - startTime);
        int minutesInt = (int)timerTime / 60;
        int secondsInt = (int)timerTime % 60;
        int seconds100Int = (int)(Mathf.Floor((timerTime - (secondsInt + minutesInt * 60)) * 100));

        if(isRunning)
        {
            timerMinutes.text = (minutesInt < 10) ? "0" + minutesInt : minutesInt.ToString();
            timerSeconds.text = (secondsInt < 10) ? "0" + secondsInt : secondsInt.ToString();
            timerSeconds100.text = (seconds100Int < 10) ? "0" + seconds100Int : seconds100Int.ToString();
        }
    }

}
