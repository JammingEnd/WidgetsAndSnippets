using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "AddContent/New Upgrade")]
public class UpgradeSCDef : ScriptableObject
{
    public List<BaseAttackSCDef> baseAttacks;

    //Dict< Index of attack, List< stat type (aka damage), value (int, bool etc)
    //Example Disc < 0 (attack 000), List< damage, 1> ----> this means that attack 000 will have its damage increased by 1
    public List<EnumWrapper> StatTypes = new List<EnumWrapper>();

    public List<ListWrapper> values = new List<ListWrapper>();
    public List<GameObject> ProjectilePrefabs;

    public string Title, Description;
    public int Price;

    public GameObject UpgradeModel;

    public GameObject SpecialtyScript;

    #region Editor

#if UNITY_EDITOR

    [CustomEditor(typeof(UpgradeSCDef))]
    public class UpgradeEditor : Editor
    {
        public void OnEnable()
        {
            UpgradeSCDef upgrade = (UpgradeSCDef)target;
            if (upgrade.baseAttacks.Safe().Any())
            {
                if (upgrade.ProjectilePrefabs.Count != upgrade.baseAttacks.Count)
                {
                    upgrade.ProjectilePrefabs.Clear();
                }
                for (int i = 0; i < upgrade.baseAttacks.Count; i++)
                {
                    upgrade.ProjectilePrefabs.Add(null);
                }
            }
            for (int i = 0; i < upgrade.baseAttacks.Count; i++)
            {
                upgrade.ProjectilePrefabs[i] = upgrade.baseAttacks[i].ProjectilePrefab;
            }
            upgrade.ProjectilePrefabs.RemoveAll(x => !x);
        }
    }

#endif

    #endregion Editor
}