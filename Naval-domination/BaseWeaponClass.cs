using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.UnitInheritScripts;

public class BaseWeaponClass : MonoBehaviour
{

   
    public float BaseAttackSpeed, 
        BaseRange, 
        BaseDamage, 
        BaseProjectileSpeed, 
        BaseRotateSpeed,
        BaseProjectilePenetration,
        BaseHitChance;
    private int TeamId = 0;   //do with setstats

    public GameObject ProjectilePrefab;
    [Header("")]
   
    public LayerMask UnitDetectMask;
    public Vector2 TurretClamp;

    public SphereCollider range;

    protected HealthManager _currentTarget;
    protected List<HealthManager> _targets = new List<HealthManager>();
    private List<float> _tagretDistance = new List<float>();
    private Vector3 _aimPoint;
    private Vector3 _targetPrevPos;
    protected Vector3 targetVelocity;

    protected float _attackSpeed, _range, _damage, _projectileSpeed, _currentRotateSpeed, _currentPenetration, _currentHitChance;
    protected int _currentClipSize, _currentBarrelIndex;
    bool _canAttack = true;
    bool _checkUpdate = false;

    public Vector3 targetDir;

    [Header("barrels")]
    public Transform BarrelsRotatePoint;
    public List<Transform> FirePoints = new List<Transform>();

    private void Update()
    {
        if(_targets.Count > 1)
        {
            ClosestsTarget();
            

        }
        if(_currentTarget != null)
        {
            RotateToTarget();
            CheckToAttack();
            RotateBarrels();
        }
    }
    private void Start()
    {
        range.enabled = true;
        
        SetStats();

        
    }

    public void SetStats()
    {
        _attackSpeed = BaseAttackSpeed;
        _range = BaseRange;
        _damage = BaseDamage;
        _projectileSpeed = BaseProjectileSpeed * 5;
        _currentRotateSpeed = BaseRotateSpeed;
        _currentPenetration = BaseProjectilePenetration;
        _currentHitChance = BaseHitChance;
        range.radius = _range;

        _currentClipSize = FirePoints.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hi");
        if(other.gameObject.TryGetComponent(out HealthManager target))
        {
            if (target.TeamId != TeamId)
            {
                _targets.Add(target);
            }
            if(_targets.Count <= 1)
            {
                _currentTarget = target;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out HealthManager enemy))
        {
            if (_targets.Contains(enemy))
            {
                _targets.Remove(enemy);
            }
        }
    }

    void ClosestsTarget()
    {
        foreach (var item in _targets)
        {
            if(item == null)
            {
                _targets.Remove(item);
            }
        }

        _tagretDistance.Clear();
        foreach (HealthManager targetPos in _targets)
        {
            float oldMin = 0;
            if (_tagretDistance.Count > 1)
            {
                oldMin = _tagretDistance.Min();
            }
            float dist = Vector3.Distance(targetPos.gameObject.transform.position, new Vector3(this.transform.position.x, 0, this.transform.position.y));
            _tagretDistance.Add(dist);
            
            if (_targets.Count > 1 && oldMin != 0)
            {
                if(oldMin > dist)
                {
                    _currentTarget = targetPos;
                }
            }
        }
        _currentTarget = _targets[_tagretDistance.IndexOf(_tagretDistance.Min())];

    }

    protected void RotateToTarget()
    {
        Vector3 aimpos = _currentTarget.gameObject.transform.position;
        /// !!!
        /// make the code for preaimging etc. use it 
        /// !!!

        var _targetPos = _currentTarget.gameObject.transform.position;
        _targetPos.y = 0;
        float step = (_currentRotateSpeed * 0.5f) * Time.deltaTime;
        
        Vector3 newDir = Vector3.RotateTowards(transform.forward, _targetPos, step, 0f);
        //newDir.y = Mathf.Clamp(this.transform.rotation.y, TurretClamp.x, TurretClamp.y);
        this.transform.rotation = Quaternion.LookRotation(newDir);
        
        RotateBarrels();
    }

    void CheckToAttack()
    {
        if (_canAttack)
        {


            Vector3 dir = (_currentTarget.transform.position - this.transform.position).normalized;
            float dotProduct = Vector3.Dot(dir, this.transform.forward);
            if(dotProduct > 0.9f)
            {
                StartCoroutine(AttackCD());

            }

        }
    }


    public virtual void RotateBarrels()
    {
        //calc height stats individually, apply in loop to all barrels
        
       
    }

    protected bool SeeIfAlive()
    {
        if(TryGetComponent(out HealthManager enemy))
        {
            if(enemy == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return false;
    }


    public virtual void DoAttack()
    {

    }

    public virtual void Fire()
    {

    }

    private IEnumerator AttackCD()
    {
        _canAttack = false;
        
        DoAttack();
        if(_currentClipSize > 1)
        {
            yield return new WaitForSeconds(_attackSpeed * 2);
        }
        else {
            yield return new WaitForSeconds(_attackSpeed);
        }
        _canAttack = true;
        StopAllCoroutines();
    }

   
}
