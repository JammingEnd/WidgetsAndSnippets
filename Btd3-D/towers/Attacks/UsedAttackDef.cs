using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UsedAttackDef
{
    public int Damage;
    public int Pierce;
    public float AttackSpeed;
    public float Range;
    public float ProjectileSpeed;
    public float Spread;
    public float LifeTime;
    public bool IsInstant;
    public bool CanSeeCamo;
    public bool CanDamageFrozen;
    public bool CanDamageLead;
    public bool CanDamagePurple;
    public bool CanRehit;


    public int AoeDamage;
    public float AoeRadius;
    public int AoePP;

    public int DamageVsFrozen,
      DamageVsLead,
      DamageVsPurple,
      DamageVsCeramic,
      DamageVsFortified,
      DamageVsMoab,
      DamageVsBoss;

    public bool isUpdated = false;

    public GameObject ProjectilePrefab;
    public GameObject SpecialScript;
    public void SetBaseStats(BaseAttackSCDef BaseAttack)
    {
        Damage = BaseAttack.BaseDamage;
        Pierce = BaseAttack.BasePierce;
        DamageVsFrozen = BaseAttack.DamageVsFrozen;
        DamageVsLead = BaseAttack.DamageVsLead;
        DamageVsPurple = BaseAttack.DamageVsPurple;
        DamageVsCeramic = BaseAttack.DamageVsCeramic;
        DamageVsFortified = BaseAttack.DamageVsFortified;
        DamageVsMoab = BaseAttack.DamageVsMoab;
        DamageVsBoss = BaseAttack.DamageVsBoss;

        AttackSpeed = BaseAttack.BaseAttackSpeed;
        Range = BaseAttack.BaseRange;
        ProjectileSpeed = BaseAttack.BaseProjectileSpeed;
        Spread = BaseAttack.BaseSpread;
        LifeTime = BaseAttack.BaseLifeTime;

        IsInstant = BaseAttack.IsInstant;
        CanSeeCamo = BaseAttack.CanSeeCamo;
        CanDamageFrozen = BaseAttack.CanDamageFrozen;
        CanDamageLead = BaseAttack.CanDamageLead;
        CanDamagePurple = BaseAttack.CanDamagePurple;
        CanRehit = BaseAttack.CanRehit;


        AoeDamage = BaseAttack.AoeDamage;
        AoePP = BaseAttack.AoePierce;
        AoeRadius = BaseAttack.AoeRadius;

        ProjectilePrefab = BaseAttack.ProjectilePrefab;
    }

    public void UpgradeStats(EnumWrapper enums, ListWrapper values)
    {
        var types = enums.Types;
        for (int i = 0; i < types.Count; i++)
        {
            StatTypes switchTypes = types[i];
            Debug.Log($"Type: {switchTypes}, Value; {values.values[i]}");
            switch (switchTypes)
            {
                case StatTypes.BaseDamage:
                case StatTypes.BasePierce:
                case StatTypes.AoeDamage:
                case StatTypes.AoePP:
                case StatTypes.DamageVsFrozen:
                case StatTypes.DamageVsLead:
                case StatTypes.DamageVsPurple:
                case StatTypes.DamageVsCeramic:
                case StatTypes.DamageVsFortified:
                case StatTypes.DamageVsMoab:
                case StatTypes.DamageVsBoss:
                    {
                        //ints
                        var fieldName = types[i].ToString().RemoveWord("Base");
                        var field = this.GetType().GetField(fieldName);
                        int newValue;
                        if (field.GetValue(this).GetType() == typeof(int))
                        {
                             newValue = (int)field.GetValue(this) + 1 * (int)values.values[i];
                        }
                        else
                        {
                             newValue = (int)field.GetValue(this) + Mathf.RoundToInt(1 * (float)values.values[i]);
                        }
                        field.SetValue(this, newValue);
                    }
                    break;
                case StatTypes.BaseAttackSpeed:
                case StatTypes.BaseRange:
                case StatTypes.BaseProjectileSpeed:
                case StatTypes.BaseSpread:
                case StatTypes.BaseLifeTime:
                case StatTypes.AoeRadius:
                    {
                        var fieldName = types[i].ToString().RemoveWord("Base");
                        var field = this.GetType().GetField(fieldName);
                        var oldvalue = (float)field.GetValue(this);
                        float newValue;
                        if (fieldName == "AttackSpeed")
                        {
                             newValue = oldvalue *  (1 - values.values[i]);

                        }
                        else
                        {
                             newValue = oldvalue * (1 + values.values[i]);
                        }
                        field.SetValue(this, newValue);
                    }
                    break;
                case StatTypes.IsInstant:
                case StatTypes.CanSeeCamo:
                case StatTypes.CanDamageFrozen:
                case StatTypes.CanDamageLead:
                case StatTypes.CanDamagePurple:
                case StatTypes.CanRehit:
                    {
                        //bools
                        var field = this.GetType().GetField(types[i].ToString());
                        Debug.Log($"bool type = {field} -- bool value = {values.values[i]}");
                        bool newValue;
                        if ((int)values.values[i] == 1)
                        {
                             newValue = true;

                        }
                        else
                        {
                            newValue = false;
                        }
                        field.SetValue(this, newValue);
                    }
                    break;
                case StatTypes.ProjectilePrefab:
                    break;
               
            }
        }
       
    }
}
