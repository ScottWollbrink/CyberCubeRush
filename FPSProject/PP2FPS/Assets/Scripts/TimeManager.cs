using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    [SerializeField] HighScores[] levelTimes;

    private float currentTime = 0;
    public bool isCounting = false;

    private void Awake()
    {      
        Instance = this;
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

    public void ToggleTimer()
    {
        isCounting = !isCounting;
    }
}
