using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBombSkill : BaseSkill
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).transform.parent = null;
            GiveDamageOnPoint();
            Destroy(gameObject);
        }
    }

    public override void GiveDamageOnPoint()
    {
        base.GiveDamageOnPoint();
    }
}
