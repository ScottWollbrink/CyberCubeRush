using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using System.Reflection;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] GameObject button;
    [SerializeField] GameObject[] levelBtns;
    private int numberOfScenes;
    
    void Start()
    {
        numberOfScenes = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < levelBtns.Length; i++)
        {
            if (!TimeManager.Instance.GetLevelUnlocked(i+1))
            {
                levelBtns[i].GetComponent<UnityEngine.UI.Image>().color = Color.grey;
                
            }
            levelBtns[i].transform.Find("prTime").GetComponent<TMP_Text>().text = TimeManager.Instance.GetPlayerLevelPR(i+1);
            levelBtns[i].transform.Find("goalTime").GetComponent<TMP_Text>().text = TimeManager.Instance.GetLevelTime(i + 1);
        }

    }

    
}
