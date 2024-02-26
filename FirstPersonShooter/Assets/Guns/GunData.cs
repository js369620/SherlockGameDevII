using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Gun Data")]
public class GunData : ScriptableObject
{
    public float range = 1000f;
    public int ammoPerClip = 12; //It's a magazine, actually
    public bool automatic = false;
    public float primaryFireDelay = 0.5f;
    [Range(0f, 90f)] public float spread = 0.0f;
}
