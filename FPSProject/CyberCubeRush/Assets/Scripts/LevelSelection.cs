using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

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
    {//more code needs to be modified to add event listeners for onclikc and on enter for each button
        GameObject btn = Instantiate(button);
        btn.name = $"btn_{index}";
        btn.GetComponentInChildren<TextMeshProUGUI>().text = index.ToString();
        btn.transform.SetParent(transform);

        if (TimeManager.Instance.GetLevelUnlocked(index))
        {
            btn.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.SelectLevel(index));
            btn.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.audioManager.buttonSounded());
        }
        else
            btn.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
        btn.GetComponent<Button>().AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => GameManager.Instance.audioManager.buttonEntered());
        btn.GetComponent<Button>().GetComponent<EventTrigger>();
        btn.transform.Find("prTime").GetComponent<TMP_Text>().text = TimeManager.Instance.GetPlayerLevelPR(index);
        btn.transform.Find("goalTime").GetComponent<TMP_Text>().text = TimeManager.Instance.GetLevelTime(index); 
    }
}
