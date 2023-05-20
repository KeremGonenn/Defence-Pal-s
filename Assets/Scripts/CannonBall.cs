using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private GameObject _explosionEffect;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private float _damage;
    private void Start()
    {
        _explosionEffect = transform.GetChild(0).gameObject;

        StartCoroutine(CheckOnGround());
    }

    private IEnumerator CheckOnGround()
    {
        while (transform.position.y >= 0.15)
        {
            yield return null;
        }
        _explosionEffect.SetActive(true);
        _explosionEffect.transform.parent = null;

        GiveDamage();

        Destroy(gameObject,0.2f);
    }

    private void GiveDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius, _interactableLayer);
        foreach (var hitCollider in hitColliders)
        {
            //Düþmanýn health scriptine eriþ ve damage ver.
        }
    }
}
