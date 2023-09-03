using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "AddContent/New Attack")]
public class BaseAttackSCDef : ScriptableObject
{
    [Header("Base Stats")]
    public int BaseDamage;
    public int BasePierce;
    public int DamageVsFrozen;
    public int DamageVsLead;
    public int DamageVsPurple;
    public int DamageVsCeramic;
    public int DamageVsFortified;
    public int DamageVsMoab;
    public int DamageVsBoss;
    public float BaseAttackSpeed;
    public float BaseRange;
    public float BaseProjectileSpeed;
    public float BaseSpread;
    public float BaseLifeTime;
    public bool IsInstant;
    public bool CanSeeCamo;
    public bool CanDamageFrozen;
    public bool CanDamageLead;
    public bool CanDamagePurple;
    public bool CanRehit;

    [Header("Special Script")]
    public GameObject SpecialScript;

    [Header("Explosive Stats")]
    public int AoeDamage;
    public float AoeRadius;
    public int AoePierce;

    [Header("PojectilePrefab")]
    public GameObject ProjectilePrefab;
    // [Header("Debuffs")]
    // public List<DebuffDef> Debuffs = new List<DebuffDef>();

    [Header("Misc")]
    public string AttackName;
}
