using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpecialTriggerAble
{
    public ISpecialTriggerAble Trigger(GameObject obj)
    {
        return null;
    }
}

public interface ISpecialTriggerAbleOnHit
{
    public ISpecialTriggerAbleOnHit TriggerOnHit(GameObject obj)
    {
        return null;
    }
}

