using System;
using System.Xml.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    GameObject activeMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject levelSelectMenu;
    [SerializeField] GameObject winMenu;

    private GameObject player;

    public bool isPaused;


    void Awake()
    {
        Instance = this;
        player = GameObject.FindWithTag("Player");
    }


    void Update()
    {        
        HandlePause(); // escape = pause
    }

    private void HandlePause()
    {
        if (Input.GetButtonDown("Cancel")) // button input
        {
            if (!isPaused) // Not paused
            {
                if (activeMenu != null) // if active menu close menu
                {
                    activeMenu.SetActive(false);
                    activeMenu = null;
                }

                statePaused();
                activeMenu = pauseMenu;
                activeMenu.SetActive(isPaused);
            }
            else // paused
            {
                stateUnpaused();
            }
        }
    }

    public void statePaused()
    {
        isPaused = !isPaused;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void stateUnpaused()
    {
        isPaused = !isPaused;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        activeMenu.SetActive(isPaused);
        activeMenu = null;
    }

    public void ReturnToPause()
    {
        SwitchScene(pauseMenu);
    }

    public void DisplayLevelSelect()
    {
        SwitchScene(levelSelectMenu);
    }

    private void SwitchScene(GameObject scene)
    {
        activeMenu.SetActive(false);
        activeMenu = scene;
        activeMenu.SetActive(true);
    }
}
