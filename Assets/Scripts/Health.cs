using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //[SerializeField] private float _health;
    [SerializeField] private float _healthValue;

    public float GetHealth()
    {
        return _healthValue;
    }

    public void ReduceHealth(float value)
    {
        _healthValue -= value;

        if(_healthValue <= 0)
        {
            EnemyDetector.Instance.GetEnemies().Remove(GetComponent<Enemy>());
            Destroy(gameObject);
        }

    }
}
