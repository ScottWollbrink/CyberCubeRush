using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.UI;
using System.Linq.Expressions;
using TMPro;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] GameObject button;

    private int numberOfScenes;
    
    void Start()
    {
        numberOfScenes = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < numberOfScenes; i++)
        {
            CreateButton(i);
        }
    }

    private void CreateButton(int index)
    {
        GameObject btn = Instantiate(button);
        int i = index + 1;
        btn.name = $"btn_{i}";
        btn.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
        btn.transform.SetParent(transform);

        btn.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.SelectLevel(index));
    }
}
