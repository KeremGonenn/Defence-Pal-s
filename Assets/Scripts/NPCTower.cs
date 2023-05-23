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

            bullet.GetComponent<TowerBullet>().Initialize(_targetEnemy,_damage);
                           
            //bullet.transform.DOMove(_targetEnemy.transform.position, .5f).OnComplete(() =>
            //{
            //    if(_targetEnemy.Health.GetHealth() > 0)
            //    {
            //        _targetEnemy.Health.ReduceHealth(_damage);

            //        //if (_targetEnemy.Health.GetHealth() <= 0)
            //        //{
            //        //    _enemyDetector.GetEnemies().RemoveAt(index);
            //        //    Destroy(_targetEnemy.gameObject);
            //        //}
            //    }
            //});
            StartCoroutine(SetAttackableState());

        }
        StartCoroutine(CheckEnemies());
    }


    private IEnumerator SetAttackableState()
    {
        yield return new WaitForSeconds(_attackTimer);
        _isAttackable = true;
    }

    public void ReducecAttackTime(float timer)
    {
        _attackTimer -= timer;
        if (_attackTimer <= .1f)
        {
            _attackTimer = 0.1f;
        }
    }

}
