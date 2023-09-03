using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileDef : MonoBehaviour
{
   // [Header("Stats")]
    private int Damage, Pierce;
    private float ProjectileSpeed, LifeTime;
    private bool IsInstant;
  //  [Header("Explosive Stats")]
    private int AoeDamage;
    private float AoeRadius;
    private int AoePierce;

    public LayerMask HitLayers;
    // [Header("Debuffs")]
    // public List<DebuffDef> Debuffs = new List<DebuffDef>();

    private Collider[] hitTargets;


    private GameObject _specialScript;
    public void SetStats(
        int damage_,
        int pierce_,
        float projectileSpeed,
        float lifeTime_,
        bool isInstant_,
        int aoeDamage_,
        float aoeRadius_,
        int aoePierce_,
        // add more here

        GameObject specialScript = null
        )
    {
        Damage = damage_;
        Pierce = pierce_;
        ProjectileSpeed = projectileSpeed * 10;
        LifeTime = lifeTime_;
        IsInstant = isInstant_;
        AoeDamage = aoeDamage_;
        AoeRadius = aoeRadius_;
        AoePierce = aoePierce_;


        _specialScript = specialScript;
    }

    private void Start()
    {
        Destroy(this.gameObject, LifeTime);
        
    }

    private void Update()
    {
        this.transform.position += this.transform.forward * ProjectileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Hit(other);
    }

    private void Hit(Collider target)
    {
        Debug.Log("Hit");
        
        if(AoePierce > 0)
        {
            AoeExplode();
        }
        else
        {
            if (target.TryGetComponent(out BloonStats bloonStats))
            {
                bloonStats.Damage(Damage);
            }

        }

        Pierce--;

        if(Pierce <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void AoeExplode()
    {
        hitTargets = Physics.OverlapSphere(this.transform.position, AoeRadius, HitLayers);
        IDictionary<GameObject, float> pierceCalcDict = new Dictionary<GameObject, float>();
        foreach (var target in hitTargets)
        {
            pierceCalcDict.Add(target.gameObject, Vector3.Distance(this.transform.position, target.transform.position));
        }
        
        var sortedList = pierceCalcDict.OrderBy(d => d.Value).ToList();
        if(sortedList.Count > AoePierce)
        {
            sortedList.RemoveAll(x => x.Value > sortedList[AoePierce - 1].Value);

        }
        //sortedList.RemoveRange(AoePierce, sortedList.Count - 1 - AoePierce);
        foreach (var target in sortedList)
        {
            var collider = target.Key;
            if(collider.TryGetComponent(out BloonStats bloonStats))
            {
                bloonStats.Damage(AoeDamage);
            }
        }
        
    }

    
}
