using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttackingDef : MonoBehaviour
{
    public List<UsedAttackDef> AttackTypes = new List<UsedAttackDef>();

    public Transform FirePosition;
    private Dictionary<UsedAttackDef, bool> _canAttackStorage = new Dictionary<UsedAttackDef, bool>();

    private List<Transform> _currentTarget = new List<Transform>();

    public BaseAttackSCDef baseAttack;
    public void AddAttacks(UsedAttackDef attack)
    {
        AttackTypes.Add(attack);
        _canAttackStorage.Add(attack, true);
        StartCoroutine(AttackCooldown(attack));
        
    }

    private void Start()
    {
        AddAttacks(SetStartAttack());
        if(this.TryGetComponent(out SphereCollider sphere))
        {
            sphere.radius = AttackTypes[0].Range;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _currentTarget.Add(other.transform);
        _currentTarget.RemoveAll(x => !x);
        Debug.Log("in range");
    }
    private void OnTriggerExit(Collider other)
    {
        _currentTarget.Remove(other.transform);
        _currentTarget.RemoveAll(x => !x);
    }

    private void Update()
    {
        if(_currentTarget.Count != 0)
        {
            this.transform.LookAt(_currentTarget[0]);
        }
    }
    private UsedAttackDef SetStartAttack()
    {
        var newAttack = new UsedAttackDef();
        newAttack.SetBaseStats(baseAttack);
        
        return newAttack;
    }

    private IEnumerator AttackCooldown(UsedAttackDef attack)
    {
   
        while (true)
        {
            _currentTarget.RemoveAll(x => !x);
            if (_currentTarget.Count > 0)
            {
                var projectileSpawn = Instantiate(attack.ProjectilePrefab, FirePosition.position, FirePosition.rotation);
                if (projectileSpawn.TryGetComponent(out ProjectileDef projectile))
                {
                    projectile.SetStats(
                            attack.Damage,
                            attack.Pierce,
                            attack.ProjectileSpeed,
                            attack.LifeTime,
                            attack.IsInstant,
                            attack.AoeDamage,
                            attack.AoeRadius,
                            attack.AoePP,

                            attack == null ? null : attack.SpecialScript
                        );
                    TriggerSpecialOnStart(AttackTypes.IndexOf(attack), projectileSpawn);
                }
               
                yield return new WaitForSeconds(attack.AttackSpeed);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
            
        }

    }



    #region Upgrade related

    public void Upgrade(List<UsedAttackDef> attacks)
    {
        foreach (var item in attacks)
        {
            if (!AttackTypes.Contains(item))
            {
                AttackTypes.Add(item);
            }
            else
            {

            }

        }
    }

    public void ResetAttackCoroutine()
    {
        this.StopAllCoroutines();
        for (int i = 0; i < AttackTypes.Count; i++)
        {
            StartCoroutine(AttackCooldown(AttackTypes[i]));
        }
    }

    #endregion

    void TriggerSpecialOnStart(int attackIndex, GameObject attack)
    {
        if (AttackTypes[attackIndex].SpecialScript != null)
        {
            if (AttackTypes[attackIndex].SpecialScript.TryGetComponent(out MonoBehaviour special))
            {
                Debug.LogWarning(special.GetType());
                attack.gameObject.AddComponent(special.GetType());
                if(attack.TryGetComponent(out ISpecialTriggerAble specialTrigger))
                {
                    specialTrigger.Trigger(attack);
                }
            }
        }

    }
}

