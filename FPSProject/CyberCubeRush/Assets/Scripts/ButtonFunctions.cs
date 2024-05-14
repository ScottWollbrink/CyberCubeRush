using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void Resume()
    {
        GameManager.Instance.stateUnpaused();
    }

    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.Instance.stateUnpaused();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.Instance.stateUnpaused();
    }

    public void LevelSelect()
    {
        GameManager.Instance.DisplayLevelSelect();
    }

    public void Settings()
    {
        GameManager.Instance.DisplaySettings();
    }

    public void ToggleReticle()
    {
        GameManager.Instance.SetReticle();
    }

    public void ToggleMouseInvert()
    {
        GameManager.Instance.SetMouseInvert();
    }

    public void ResetSettings()
    {
        GameManager.Instance.ResetSettings();
    }

    public void Return()
    {
        GameManager.Instance.ReturnToPause();
    }

    public void ReturnToSettings()
    {
        GameManager.Instance.ReturnToSettings();
    }

    public void SelectLevel(int level)
    {
        SceneManager.LoadScene(level);
        GameManager.Instance.stateUnpaused();
    }

    public void ShowCreditrs()
    {
        GameManager.Instance.ShowCredits();
    }

    public void Respawn()
    {
        GameManager.Instance.playerCntrl.SpawnPlayer();
        GameManager.Instance.stateUnpaused();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
