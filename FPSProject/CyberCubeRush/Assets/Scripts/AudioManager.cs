using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;
using Button = UnityEngine.UI.Button;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Sources ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] List<AudioSource> sfxSources;
    [SerializeField] AudioSource UISource;

    [Header("---------- Music ----------")]
    [SerializeField] AudioClip[] tunes;

    [Header("---------- Volumes ----------")]
    [SerializeField] public float musVol;
    [SerializeField] public float sfxVol;
    [SerializeField] public float UIVol;
    [SerializeField] public Slider[] sliders;

    [Header("---------- UI Sounds ----------")]
    [SerializeField] AudioClip buttonHover;
    [SerializeField] AudioClip buttonClick;
    [SerializeField] AudioClip readyBeeps;
    [SerializeField] AudioClip goBeep;

    [Header("---------- Settings ----------")]
    [SerializeField] SettingsSO defaultSettings;
    [SerializeField] SettingsSO userSettings;

    [Header("---------- Texts ----------")]
    [SerializeField] TMP_Text musicVolText;
    [SerializeField] TMP_Text sfxVolText;
    [SerializeField] TMP_Text UIVolText;

    [Header("---------- Buttons ----------")]
    [SerializeField] List<Button> buttons;
    private void Awake()
    {
        sfxSources = new List<AudioSource>(AudioSource.FindObjectsOfType(typeof(AudioSource)) as AudioSource[]);
        buttons = new List<Button>(Button.FindObjectsOfType(typeof(Button)) as Button[]);
        GameObject uitemp = GameObject.FindWithTag("UI");
        musVol = userSettings.musicVolume;
        sfxVol = userSettings.sfxVolume;
        UIVol = userSettings.UIVolume;
        sfxSources.Remove(uitemp.GetComponent<AudioSource>());
        sfxSources.Remove(musicSource.GetComponent<AudioSource>());
        musicSource.volume = musVol;
        for(int x = 0; x < sfxSources.Count; x++) 
        {
            sfxSources[x].volume = sfxVol;
        }
        
        UISource.volume = UIVol;
        sfxVolText.text = sfxVol.ToString("F2");
        musicVolText.text = musVol.ToString("F2");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void sfxVolSet(float value)
    {
        sfxVol = value;
        userSettings.sfxVolume = sfxVol;
        for (int x = 0; x < sfxSources.Count; x++)
        {
            sfxSources[x].volume = sfxVol;
            //Debug.Log(sliders[x].name + " value changed to: " + value.ToString("F2"));
            Debug.Log("sfxvolset used");
            Debug.Log(x);
        }
        sfxVolText.text = sfxVol.ToString("F2");
    }
    public void musicVolSet(float value)
    {

        musVol = value;
        userSettings.musicVolume = musVol;
        musicSource.volume = musVol;
        musicVolText.text = musVol.ToString("F2");
    }
    public void UIVolSet(float value)
    {

        UIVol = value;
        userSettings.UIVolume = UIVol;
        UISource.volume = UIVol;
        UIVolText.text = UIVol.ToString("F2");
    }
    public void SliderValueChanged(GameObject slider, float value)
    {
        
    }
    public void buttonEntered()
    {
        UISource.PlayOneShot(buttonHover, UIVol);
    }
    public void countingDownBeepPlay()
    {
        UISource.PlayOneShot(readyBeeps, UIVol);
    }
    public void startBeepPlay()
    {
        UISource.PlayOneShot(goBeep, UIVol);
    }
    public void buttonSounded()
    {
        UISource.PlayOneShot(buttonClick, UIVol);
    }
}
