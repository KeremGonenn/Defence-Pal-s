using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    private Enemy _target;

    [SerializeField] private float _damage;

    [SerializeField] private float _speed;

    public void Initialize(Enemy target,float damage)
    {
        _target = target;
       // _damage = damage;
    }

    public void SetDamage(float damageValue)
    {
        _damage += damageValue;
    }
    private void Update()
    {
        if (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);

            if(Vector3.Distance(transform.position,_target.transform.position) < .075f)
            {
                _target.Health.ReduceHealth(_damage);
                _target.PlayBloodParticle();
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
