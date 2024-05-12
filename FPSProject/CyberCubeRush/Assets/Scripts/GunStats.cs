using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class GunStats : ScriptableObject
{
    public GameObject gunModel;
    public int damage;
    public float rateOfFire;
    public int range;
    public int ammoCurrent;
    public int ammoMax;

    public ParticleSystem hitEffect;
    public AudioClip shootSound;
    [Range(0f, 1f)] public float shootShoundVolume;
}
