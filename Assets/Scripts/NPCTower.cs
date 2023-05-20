using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTower : MonoBehaviour
{
    [SerializeField] EnemyDetector _enemyDetector;

    [SerializeField] private float _checkTimer;

    private Enemy _targetEnemy;



    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(_checkTimer);

        int index = 0;

        if (_enemyDetector.GetEnemies().Count >= 0)
        {
            if (_enemyDetector.GetEnemies()[index].isTargetable)
            {
                _targetEnemy = _enemyDetector.GetEnemies()[index];
            }
            else
            {
                index += 1;
                _targetEnemy = _enemyDetector.GetEnemies()[index];
            }
        }
    }
}
