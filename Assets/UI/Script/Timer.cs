using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;
    public ScoreData scoreData;

    public bool isWinFlag=false;

    void Update()
    {

        if (isWinFlag == true)
        {
            return;
        }

        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{00:00}:{01:00}", minutes, seconds);
        scoreData.TimerScore = elapsedTime;
    }
}
