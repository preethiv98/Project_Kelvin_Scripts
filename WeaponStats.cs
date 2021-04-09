using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Weapon",menuName ="Scriptable Objects/Weapon")]
public class WeaponStats : ScriptableObject
{
    public GameObject weaponPrefab;
    public Sprite weaponSprite;
    public int damage;
    public float occuranceRate;
    public bool plasmaDamage, electricDamage;
}
