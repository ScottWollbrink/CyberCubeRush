using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    [Header("---------- UI Sounds ----------")]
    [SerializeField] AudioClip buttonHover;
    [SerializeField] AudioClip buttonClick;
    [SerializeField] AudioClip readyBeeps;
    [SerializeField] AudioClip goBeep;
    private void Awake()
    {
        sfxSources = new List<AudioSource>(AudioSource.FindObjectsOfType(typeof(AudioSource)) as AudioSource[]);
        GameObject temp = GameObject.FindWithTag("UI");
        sfxSources.Remove(temp.GetComponent<AudioSource>());
        musicSource.volume = musVol;
        for(int x = 0; x < sfxSources.Count; x++) 
        {
            sfxSources[x].volume = sfxVol;
        }
        UISource.volume = UIVol;
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
        for (int x = 0; x < sfxSources.Count; x++)
        {
            sfxSources[x].volume = sfxVol;
        }
    }
}
