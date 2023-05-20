using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    private List<Enemy> _enemies;

    private void Start()
    {
        _enemies = new List<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            _enemies.Add(enemy);
        }
    }

    public List<Enemy> GetEnemies()
    {
        return _enemies;
    }

}
