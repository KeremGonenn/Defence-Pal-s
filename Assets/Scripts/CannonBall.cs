using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private GameObject _explosionEffect;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private float _damage;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (_explosionEffect != null)
            {
                _explosionEffect.SetActive(true);
                _explosionEffect.transform.parent = null;
            }


            if (SkillSystem.Instance.skillEnums == SkillEnums.BallExplosion)
            {
                SkillFunctions.Instance.skillPrefab.SkillPosition = transform.position;
                SkillSystem.Instance.ExecuteSkill();
                SkillSystem.Instance.skillEnums = SkillEnums.None;
            }

            GiveDamage();

            Destroy(gameObject, 0.2f);
            AudioManager.instance.PlaySFX("BlastSound");
        }
    }

    public void SetDamage(float damageValue)
    {
        _damage += damageValue;
    }
    private void GiveDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius, _interactableLayer);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.ReduceHealth(_damage);
            }
        }
    }
}
