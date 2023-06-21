using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    public float damage;
    public float destroyTime;
    public LayerMask targetLayer;
    public float effectRadius;
    public float effectFrequency;

    public virtual void GiveDamageOnPoint()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, effectRadius,targetLayer);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.GetComponent<Health>().ReduceHealth(damage);
        }
    }
}
