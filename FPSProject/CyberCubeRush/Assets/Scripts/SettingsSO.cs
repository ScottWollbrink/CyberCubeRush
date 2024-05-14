using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SettingsSO : ScriptableObject
{
    public bool showReticle = true;
    public bool invertMouse = false;
    public float mouseSense = 100f;
    public float musicVolume = 50f;
    public float sfxVolume = 50f;
}
