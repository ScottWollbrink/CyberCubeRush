using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;
using Button = UnityEngine.UI.Button;
using UnityEngine.SceneManagement;

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
    [SerializeField] public float masterVol;
    [SerializeField] public Slider[] sliders;

    [Header("---------- UI Sounds ----------")]
    [SerializeField] AudioClip buttonHover;
    [SerializeField] AudioClip buttonClick;
    [SerializeField] AudioClip readyBeeps;
    [SerializeField] AudioClip goBeep;
    [SerializeField] AudioClip victory;
    [SerializeField] AudioClip checkpoint;

    [Header("---------- Settings ----------")]
    [SerializeField] SettingsSO defaultSettings;
    [SerializeField] SettingsSO userSettings;

    [Header("---------- Texts ----------")]
    [SerializeField] TMP_Text masterVolText;
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
        masterVol = userSettings.masterVolume;
        sfxSources.Remove(uitemp.GetComponent<AudioSource>());
        sfxSources.Remove(musicSource.GetComponent<AudioSource>());
        musicSource.volume = musVol * masterVol;
        musicSource.clip = tunes[SceneManager.GetActiveScene().buildIndex % 5];
        musicSource.Play();
        for (int x = 0; x < sfxSources.Count; x++) 
        {
            sfxSources[x].volume = sfxVol * masterVol;
            if (sfxSources[x].CompareTag("Lazer"))
            {
                sfxSources[x].volume *= .2f;
            }
        }
        
        UISource.volume = UIVol * masterVol;
        sfxVolText.text = sfxVol.ToString("F2");
        musicVolText.text = musVol.ToString("F2");
        masterVolText.text = masterVol.ToString("F2");
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
            sfxSources[x].volume = sfxVol * masterVol;
            if (sfxSources[x].CompareTag("Lazer"))
            {
                sfxSources[x].volume *= .20f;
            }
        }
        sfxVolText.text = sfxVol.ToString("F2");
    }
    public void musicVolSet(float value)
    {

        musVol = value;
        userSettings.musicVolume = musVol;
        musicSource.volume = musVol * masterVol;
        musicVolText.text = musVol.ToString("F2");
    }
    public void UIVolSet(float value)
    {

        UIVol = value;
        userSettings.UIVolume = UIVol;
        UISource.volume = UIVol * masterVol;
        UIVolText.text = UIVol.ToString("F2");
        UISource.PlayOneShot(buttonHover);
    }
    public void masterVolSet(float value)
    {
        masterVol = value;
        userSettings.masterVolume = masterVol;
        //setting all sources by master vol
        UISource.volume = UIVol * masterVol;
        musicSource.volume = musVol * masterVol;
        masterVolText.text = masterVol.ToString("F2");
        for (int x = 0; x < sfxSources.Count; x++)
        {
            sfxSources[x].volume = sfxVol * masterVol;
            
            if (sfxSources[x].CompareTag("Lazer"))
            {
                sfxSources[x].volume *= .20f;
            }
        }
        UISource.PlayOneShot(buttonHover);
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
    public void playCP()
    {
        UISource.PlayOneShot(checkpoint, UIVol);
    }
    public void playVictory()
    {
        musicSource.Stop();
        musicSource.PlayOneShot(victory, musVol);
    }
}
