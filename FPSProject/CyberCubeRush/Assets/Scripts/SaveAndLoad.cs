using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    [SerializeField] SettingsSO playersSettings;
    [SerializeField] SettingsSO defaultStettings;

    [SerializeField] HighScores ben1SaveState;
    [SerializeField] HighScores ben2SaveState;
    [SerializeField] HighScores ben3SaveState;
    [SerializeField] HighScores scott1SaveState;
    [SerializeField] HighScores scott2SaveState;
    [SerializeField] HighScores scott3SaveState;
    [SerializeField] HighScores clayton1SaveState;
    [SerializeField] HighScores clayton2SaveState;
    [SerializeField] HighScores clayton3SaveState;
    [SerializeField] HighScores clayton4SaveState;
    [SerializeField] HighScores clayton5SaveState;

    public void Save()
    {
        PlayerPrefs.SetInt("Show Reticle", (playersSettings.showReticle ? 1 : 0));
        PlayerPrefs.SetInt("Invert Mouse", (playersSettings.invertMouse ? 1 : 0));
        PlayerPrefs.SetFloat("Mouse Sense", playersSettings.mouseSense);
        PlayerPrefs.SetFloat("Music Volume", playersSettings.musicVolume);
        PlayerPrefs.SetFloat("SFX Volume", playersSettings.sfxVolume);

        PlayerPrefs.SetInt("Level Unlocked Tutorial", (ben1SaveState.levelIsUnlocked ? 1 : 0));
        PlayerPrefs.SetFloat("PR Tutorial", ben1SaveState.playerPR);
        PlayerPrefs.SetInt("Level Unlocked ben2", (ben2SaveState.levelIsUnlocked ? 1 : 0));
        PlayerPrefs.SetFloat("PR ben2", ben2SaveState.playerPR);
        PlayerPrefs.SetInt("Level Unlocked ben3", (ben3SaveState.levelIsUnlocked ? 1 : 0));
        PlayerPrefs.SetFloat("PR ben3", ben3SaveState.playerPR);
        PlayerPrefs.SetInt("Level Unlocked scott1", (scott1SaveState.levelIsUnlocked ? 1 : 0));
        PlayerPrefs.SetFloat("PR scott1", scott1SaveState.playerPR);
        PlayerPrefs.SetInt("Level Unlocked scott2", (scott2SaveState.levelIsUnlocked ? 1 : 0));
        PlayerPrefs.SetFloat("PR scott2", scott2SaveState.playerPR);
        PlayerPrefs.SetInt("Level Unlocked scott3", (scott3SaveState.levelIsUnlocked ? 1 : 0));
        PlayerPrefs.SetFloat("PR scott3", scott3SaveState.playerPR);
        PlayerPrefs.SetInt("Level Unlocked clayton1", (clayton1SaveState.levelIsUnlocked ? 1 : 0));
        PlayerPrefs.SetFloat("PR clayton1", clayton1SaveState.playerPR);
        PlayerPrefs.SetInt("Level Unlocked clayton2", (clayton2SaveState.levelIsUnlocked ? 1 : 0));
        PlayerPrefs.SetFloat("PR clayton2", clayton2SaveState.playerPR);
        PlayerPrefs.SetInt("Level Unlocked clayton3", (clayton3SaveState.levelIsUnlocked ? 1 : 0));
        PlayerPrefs.SetFloat("PR clayton3", clayton3SaveState.playerPR);
        PlayerPrefs.SetInt("Level Unlocked clayton4", (clayton4SaveState.levelIsUnlocked ? 1 : 0));
        PlayerPrefs.SetFloat("PR clayton4", clayton4SaveState.playerPR);
        PlayerPrefs.SetInt("Level Unlocked clayton5", (clayton5SaveState.levelIsUnlocked ? 1 : 0));
        PlayerPrefs.SetFloat("PR clayton5", clayton5SaveState.playerPR);
    }

    public void Load()
    {
        playersSettings.showReticle = (PlayerPrefs.GetInt("Show Reticle", Convert.ToInt32(defaultStettings.showReticle)) != 0);
        playersSettings.showReticle = (PlayerPrefs.GetInt("Invert Mouse", Convert.ToInt32(defaultStettings.invertMouse)) != 0);
        playersSettings.mouseSense = PlayerPrefs.GetFloat("Mouse Sense", defaultStettings.mouseSense);
        playersSettings.musicVolume = PlayerPrefs.GetFloat("Music Volume", defaultStettings.musicVolume);
        playersSettings.sfxVolume = PlayerPrefs.GetFloat("SFX Volume", defaultStettings.sfxVolume);

        ben1SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked Tutorial", 1) != 0);
        ben1SaveState.playerPR = PlayerPrefs.GetFloat("PR Tutorial", 0f);
        ben2SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked ben2", 0) != 0);
        ben2SaveState.playerPR = PlayerPrefs.GetFloat("PR ben2", 0f);
        ben3SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked ben3", 0) != 0);
        ben3SaveState.playerPR = PlayerPrefs.GetFloat("PR ben3", 0f);
        scott1SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked scott1", 0) != 0);
        scott1SaveState.playerPR = PlayerPrefs.GetFloat("PR scott1", 0f);
        scott2SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked scott2", 0) != 0);
        scott2SaveState.playerPR = PlayerPrefs.GetFloat("PR scott2", 0f);
        scott3SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked scott3", 0) != 0);
        scott3SaveState.playerPR = PlayerPrefs.GetFloat("PR scott3", 0f);
        clayton1SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked clayton1", 0) != 0);
        clayton1SaveState.playerPR = PlayerPrefs.GetFloat("PR clayton1", 0f);
        clayton2SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked clayton2", 0) != 0);
        clayton2SaveState.playerPR = PlayerPrefs.GetFloat("PR clayton2", 0f);
        clayton3SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked clayton3", 0) != 0);
        clayton3SaveState.playerPR = PlayerPrefs.GetFloat("PR clayton3", 0f);
        clayton4SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked clayton4", 0) != 0);
        clayton4SaveState.playerPR = PlayerPrefs.GetFloat("PR clayton4", 0f);
        clayton5SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked clayton5", 0) != 0);
        clayton5SaveState.playerPR = PlayerPrefs.GetFloat("PR clayton5", 0f);
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
        Load();
    }
}
