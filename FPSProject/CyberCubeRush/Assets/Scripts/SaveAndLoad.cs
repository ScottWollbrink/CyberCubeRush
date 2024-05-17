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
        PlayerPrefs.SetInt("Level Unlocked Tutorial", (ben2SaveState.levelIsUnlocked ? 1 : 0));
        PlayerPrefs.SetFloat("PR Tutorial", ben2SaveState.playerPR);
        //PlayerPrefs.SetInt("Level Unlocked Tutorial", (ben3SaveState.levelIsUnlocked ? 1 : 0));
        //PlayerPrefs.SetFloat("PR Tutorial", ben3SaveState.playerPR);
        PlayerPrefs.SetInt("Level Unlocked Tutorial", (scott1SaveState.levelIsUnlocked ? 1 : 0));
        PlayerPrefs.SetFloat("PR Tutorial", scott1SaveState.playerPR);
        PlayerPrefs.SetInt("Level Unlocked Tutorial", (scott2SaveState.levelIsUnlocked ? 1 : 0));
        PlayerPrefs.SetFloat("PR Tutorial", scott2SaveState.playerPR);
        //PlayerPrefs.SetInt("Level Unlocked Tutorial", (scott3SaveState.levelIsUnlocked ? 1 : 0));
        //PlayerPrefs.SetFloat("PR Tutorial", scott3SaveState.playerPR);
        PlayerPrefs.SetInt("Level Unlocked Tutorial", (clayton1SaveState.levelIsUnlocked ? 1 : 0));
        PlayerPrefs.SetFloat("PR Tutorial", clayton1SaveState.playerPR);
        PlayerPrefs.SetInt("Level Unlocked Tutorial", (clayton2SaveState.levelIsUnlocked ? 1 : 0));
        PlayerPrefs.SetFloat("PR Tutorial", clayton2SaveState.playerPR);
        //PlayerPrefs.SetInt("Level Unlocked Tutorial", (clayton3SaveState.levelIsUnlocked ? 1 : 0));
        //PlayerPrefs.SetFloat("PR Tutorial", clayton3SaveState.playerPR);
        //PlayerPrefs.SetInt("Level Unlocked Tutorial", (clayton4SaveState.levelIsUnlocked ? 1 : 0));
        //PlayerPrefs.SetFloat("PR Tutorial", clayton4SaveState.playerPR);
        //PlayerPrefs.SetInt("Level Unlocked Tutorial", (clayton5SaveState.levelIsUnlocked ? 1 : 0));
        //PlayerPrefs.SetFloat("PR Tutorial", clayton5SaveState.playerPR);
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
        ben2SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked Tutorial", 0) != 0);
        ben2SaveState.playerPR = PlayerPrefs.GetFloat("PR Tutorial", 0f);
        //ben3SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked Tutorial", 0) != 0);
        //ben3SaveState.playerPR = PlayerPrefs.GetFloat("PR Tutorial", 0f);
        scott1SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked Tutorial", 0) != 0);
        scott1SaveState.playerPR = PlayerPrefs.GetFloat("PR Tutorial", 0f);
        scott2SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked Tutorial", 0) != 0);
        scott2SaveState.playerPR = PlayerPrefs.GetFloat("PR Tutorial", 0f);
        //scott3SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked Tutorial", 0) != 0);
        //scott3SaveState.playerPR = PlayerPrefs.GetFloat("PR Tutorial", 0f);
        clayton1SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked Tutorial", 0) != 0);
        clayton1SaveState.playerPR = PlayerPrefs.GetFloat("PR Tutorial", 0f);
        clayton2SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked Tutorial", 0) != 0);
        clayton2SaveState.playerPR = PlayerPrefs.GetFloat("PR Tutorial", 0f);
        //clayton3SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked Tutorial", 0) != 0);
        //clayton3SaveState.playerPR = PlayerPrefs.GetFloat("PR Tutorial", 0f);
        //clayton4SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked Tutorial", 0) != 0);
        //clayton4SaveState.playerPR = PlayerPrefs.GetFloat("PR Tutorial", 0f);
        //clayton5SaveState.levelIsUnlocked = (PlayerPrefs.GetInt("Level Unlocked Tutorial", 0) != 0);
        //clayton5SaveState.playerPR = PlayerPrefs.GetFloat("PR Tutorial", 0f);
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
        Load();
    }
}
