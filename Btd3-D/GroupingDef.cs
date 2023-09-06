using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Group", menuName = "AddContent/New Group")]
public class GroupingDef : ScriptableObject
{
    public float SpawnInterval, SpawnAfter;
    public int UnitCount;



    public GameObject BloonType;
}
