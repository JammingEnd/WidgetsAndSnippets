using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePath : MonoBehaviour
{
    public List<UpgradeSCDef> Upgrades = new List<UpgradeSCDef>();

    public int _currentLevel = 0;

    private AttackingDef _attackDef;

    //private SideBarUIHandler _UiHandler;
    private void Start()
    {
        if(this.TryGetComponent(out AttackingDef def))
        {
            _attackDef = def;
        }
    }




    public void OnUpgrade_OnClick()
    {
        
        for (int i = 0; i < Upgrades[_currentLevel].baseAttacks.Count; i++)
        {
            if(_attackDef.baseAttack.ProjectilePrefab != Upgrades[_currentLevel].baseAttacks[i].ProjectilePrefab)
            {
                var newBaseAttack = Upgrades[_currentLevel].baseAttacks[i];
                _attackDef.baseAttack = newBaseAttack;
                UsedAttackDef newAttack = new UsedAttackDef();
                newAttack.SetBaseStats(newBaseAttack);
                newAttack.SpecialScript = Upgrades[_currentLevel].SpecialtyScript;
                _attackDef.AttackTypes[i] = newAttack;
                _attackDef.ResetAttackCoroutine();
            }
            else
            {
                var upgradedAttack = _attackDef.AttackTypes[i];
                Debug.Log($"iterating at index {0}");
                upgradedAttack.UpgradeStats(Upgrades[_currentLevel].StatTypes[i], Upgrades[_currentLevel].values[i]);
                upgradedAttack.SpecialScript = Upgrades[_currentLevel].SpecialtyScript;
                _attackDef.AttackTypes[i] = upgradedAttack;
                _attackDef.ResetAttackCoroutine();
            }
           
        }
       

        OnUpgrade();
    }

    private void OnUpgrade() 
    {
        _currentLevel++;

        
    }
}
