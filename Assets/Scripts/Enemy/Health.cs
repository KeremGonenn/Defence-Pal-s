using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //[SerializeField] private float _health;
    [SerializeField] private float _healthValue;

    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    public float GetHealth()
    {
        return _healthValue;
    }

    public void ReduceHealth(float value)
    {
        _healthValue -= value;

        if(_healthValue <= 0)
        {
            if (_enemy != null)
            {
                _enemy.PlayDeathParticle();
                EnemyDetector.Instance.GetEnemies().Remove(GetComponent<Enemy>());
            }
                Destroy(gameObject);
        }

    }
}
