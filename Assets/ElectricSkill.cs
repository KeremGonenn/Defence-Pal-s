using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSkill : BaseSkill
{
    private void Start()
    {
        StartCoroutine(CO_StartElectricSkill());
    }

    private IEnumerator CO_StartElectricSkill()
    {
        yield return new WaitForSeconds(effectFrequency);
        Destroy(gameObject, destroyTime);
        GiveDamageOnPoint();
        StartCoroutine(CO_StartElectricSkill());
    }

    public override void GiveDamageOnPoint()
    {
        base.GiveDamageOnPoint();
    }
}
