using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSkill : BaseSkill
{
    private HashSet<Rigidbody> _enemies;
    private void Start()
    {
        _enemies = new HashSet<Rigidbody>();


        Destroy(gameObject, destroyTime);

        //StartCoroutine(CO_Magnet());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            Vector3 direction = (transform.position - enemy.transform.position).normalized * -1;
            Rigidbody targetRb = enemy.GetComponent<Rigidbody>();
            if (!_enemies.Contains(targetRb))
            {
                Debug.Log("qqqq");
                _enemies.Add(targetRb);
                //~RigidbodyConstraints.FreezePositionX | ~RigidbodyConstraints.FreezePositionZ
                targetRb.constraints &= RigidbodyConstraints.None;
                targetRb.AddForce(direction * 1250f);
                _enemies.Remove(targetRb);
                StartCoroutine(SetFreezeState(targetRb));
            }
        }
    }

    private IEnumerator SetFreezeState(Rigidbody target)
    {
        yield return new WaitForSeconds(2f);
        target.constraints = RigidbodyConstraints.FreezeAll;
    }

    private IEnumerator CO_Magnet()
    {
        yield return null;
        //yield return new WaitForSeconds(effectFrequency);
        Magnet();
        StartCoroutine(CO_Magnet());
    }
    
    private void Magnet()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, effectRadius, targetLayer);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log(hitCollider.name);
            Vector3 direction = (transform.position - hitCollider.transform.position).normalized * -1;
            Rigidbody targetRb = hitCollider.GetComponent<Rigidbody>();
            if (!_enemies.Contains(targetRb))
            {
                Debug.Log("qqqq");
                _enemies.Add(targetRb);
                //~RigidbodyConstraints.FreezePositionX | ~RigidbodyConstraints.FreezePositionZ
                targetRb.constraints &= RigidbodyConstraints.None;
                targetRb.AddForce(direction * 500f);
            }
        }
    }
}
