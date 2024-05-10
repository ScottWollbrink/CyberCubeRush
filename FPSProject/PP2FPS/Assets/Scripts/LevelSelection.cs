using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] GameObject button;

    private int numberOfScenes;
    
    void Start()
    {
        numberOfScenes = SceneManager.sceneCountInBuildSettings;

        for (int i = 1; i < numberOfScenes; i++)
        {
            CreateButton(i);
        }
    }

    private void CreateButton(int index)
    {
        GameObject btn = Instantiate(button);
        btn.name = $"btn_{index}";
        btn.GetComponentInChildren<TextMeshProUGUI>().text = index.ToString();
        btn.transform.SetParent(transform);

        if (TimeManager.Instance.GetLevelUnlocked(index))        
            btn.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.SelectLevel(index));                               
        else        
            btn.GetComponent<Image>().color = Color.grey;

        btn.transform.Find("prTime").GetComponent<TMP_Text>().text = TimeManager.Instance.GetPlayerLevelPR(index);
        btn.transform.Find("goalTime").GetComponent<TMP_Text>().text = TimeManager.Instance.GetLevelTime(index); 
    }
}
