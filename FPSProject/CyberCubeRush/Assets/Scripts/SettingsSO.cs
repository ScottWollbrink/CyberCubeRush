using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SettingsSO : ScriptableObject
{
    public bool showReticle = true;
    public bool invertMouse = false;
    public float mouseSense = .25f;
    public float musicVolume = .25f;
    public float sfxVolume = .5f;
    public float UIVolume = .5f;
}
