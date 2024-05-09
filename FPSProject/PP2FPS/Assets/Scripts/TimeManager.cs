using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    [SerializeField] HighScores[] levelTimes;

    private float currentTime = 0;
    public bool isCounting = true;

    private void Awake()
    {      
        Instance = this;
    }

    public void SetPlayerPR(int index)
    {
        for (int i = 0; i < levelTimes.Length; i++)
        {
            if (levelTimes[i].levelBuildIndex == index)
            {
                levelTimes[i].SetPr(currentTime);
            }
        }
    }

    public bool IsPlayerPRSet(int index)
    {
        for (int i = 0; i < levelTimes.Length; i++)
        {
            if (levelTimes[i].levelBuildIndex == index)
            {
                if (levelTimes[i].playerPR > levelTimes[i].timeToBeat)
                    return false;
                else
                    return true;
            }
        }
        return false;
    }

    public string GetPlayerLevelPR(int index)
    {
        for (int i = 0; i < levelTimes.Length; i++)
        {
            if (levelTimes[i].levelBuildIndex == index)
            {
                float time = levelTimes[i].playerPR;
                if (time == float.MaxValue)
                    break;

                int minutes = (int)(time / 60);
                int seconds = (int)(time % 60);
                int milliseconds = (int)((time - Mathf.Floor(time)) * 1000);

                string timeStamp = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);

                return timeStamp;
            }
        }
        return "Time Not Set";
    }

    public string GetLevelTime(int index)
    {
        for (int i = 0; i < levelTimes.Length; i++)
        {
            if (levelTimes[i].levelBuildIndex == index)
            {
                float time = levelTimes[i].timeToBeat;

                int minutes = (int)(time / 60);
                int seconds = (int)(time % 60);
                int milliseconds = (int)((time - Mathf.Floor(time)) * 1000);

                string timeStamp = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);

                return timeStamp;
            }
        }
        return "Time Not Set";
    }

    public string GetCurrentTime()
    {
        currentTime += Time.deltaTime;

        // Calculate minutes, seconds, and milliseconds
        int minutes = (int)(currentTime / 60);
        int seconds = (int)(currentTime % 60);
        int milliseconds = (int)((currentTime - Mathf.Floor(currentTime)) * 1000);

        string timeStamp = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        return timeStamp;
    }

    public void ToggleTimer(bool val)
    {
        isCounting = val;
    }
}
