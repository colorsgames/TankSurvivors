using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Weapon Data", menuName = "Weapon Data", order = 61)]
public class WeaponData : ScriptableObject
{
    public Shell shell;
    public float minDamage;
    public float maxDamage;
    public float delay;
    public float minDelay;
    public float shotShakeIntensity;
    public float shotShakeTime;

    public AudioClip shot;
}
