using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HighScores : ScriptableObject
{
    public int levelBuildIndex = 1;
    public float timeToBeat = 60f;
    public float playerPR = float.MaxValue;

    public void SetPr(float time)
    {
        playerPR = time;
    }
}
