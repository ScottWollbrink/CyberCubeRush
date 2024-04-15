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
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject reticle;

    private GameObject player;

    public bool isPaused;
    public bool reticleIsShowing;


    void Awake()
    {
        Instance = this;
        player = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        CheckReticle();
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
                if (activeMenu == levelSelectMenu || activeMenu == settingsMenu)
                {
                    activeMenu.SetActive(!isPaused);
                    activeMenu = pauseMenu;
                    activeMenu.SetActive(isPaused);

                }
                else
                {
                    stateUnpaused();
                }
            }
        }
    }

    public void statePaused()
    {
        isPaused = !isPaused;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        reticle.SetActive(false);
    }

    public void stateUnpaused()
    {
        isPaused = !isPaused;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        activeMenu.SetActive(isPaused);
        activeMenu = null;
        if (reticleIsShowing)
        {
            reticle.SetActive(true);
        }
    }

    public void ReturnToPause()
    {
        SwitchScene(pauseMenu);
    }

    public void DisplayLevelSelect()
    {
        SwitchScene(levelSelectMenu);
    }

    public void DisplaySettings()
    {
        SwitchScene(settingsMenu);
    }

    private void SwitchScene(GameObject scene)
    {
        activeMenu.SetActive(false);
        activeMenu = scene;
        activeMenu.SetActive(true);
    }

    private void CheckReticle()
    {
        reticleIsShowing = reticle.activeSelf;
    }

    public void SetReticle()
    {
        reticleIsShowing = !reticleIsShowing;
    }

    private void ToggleReticle()
    {
        SetReticle();
        reticle.SetActive(reticleIsShowing);
    }

    public void WinGame()
    {
        statePaused();

        activeMenu = winMenu;
        activeMenu.SetActive(isPaused);
    }
}
