using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bloon Group", menuName = "AddContent/New Group")]
public class BloonGroupingDef : ScriptableObject
{
    public float SpawnInterval, SpawnAfter;
    public int BloonCount;



    public GameObject BloonType;
}
