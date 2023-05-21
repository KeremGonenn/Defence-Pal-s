using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPCTower : MonoBehaviour
{
    [SerializeField] EnemyDetector _enemyDetector;

    [SerializeField] private float _checkTimer;

    [SerializeField] private GameObject _bullet;

    [SerializeField] private Transform _shootPoint;

    [SerializeField] private float _damage;

    [SerializeField] private Enemy _targetEnemy;

    [SerializeField] private float _attackTimer;

    private bool _isAttackable = true;
     
    private void Start()
    {
        StartCoroutine(CheckEnemies());
    }


    IEnumerator CheckEnemies()
    {

        yield return new WaitForSeconds(_checkTimer);

        if (!_isAttackable)
        {
            StartCoroutine(CheckEnemies());
            yield break;
        }

        int index = 0;

        if (_enemyDetector.GetEnemies().Count > 0)
        {
            if (_enemyDetector.GetEnemies()[index].isTargetable)
            {
                _targetEnemy = _enemyDetector.GetEnemies()[index];
            }
            else
            {
                if (_enemyDetector.GetEnemies().Count == 1)
                {
                    _targetEnemy = _enemyDetector.GetEnemies()[index];
                }
                else
                {
                    index += 1;
                    _targetEnemy = _enemyDetector.GetEnemies()[index];

                }

                //if (index >= _enemyDetector.GetEnemies().Count)
                //{
                //}
            }
        }

        yield return null;

        if (_targetEnemy != null && _isAttackable)
        {
            _isAttackable = false;
            var bullet = Instantiate(_bullet, _shootPoint.position, Quaternion.identity);
            bullet.transform.DOMove(_targetEnemy.transform.position, .5f).OnComplete(() =>
            {
                if(_targetEnemy.GetHealth() > 0)
                {
                    _targetEnemy.ReduceHealth(_damage);

                    if (_targetEnemy.GetHealth() <= 0)
                    {
                        _enemyDetector.GetEnemies().RemoveAt(index);
                        Destroy(_targetEnemy.gameObject);
                    }
                }
            });
            StartCoroutine(SetAttackableState());

        }
        StartCoroutine(CheckEnemies());
    }


    private IEnumerator SetAttackableState()
    {
        yield return new WaitForSeconds(_attackTimer);
        _isAttackable = true;
    }
}
