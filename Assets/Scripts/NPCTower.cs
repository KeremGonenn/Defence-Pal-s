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

    [SerializeField] private Transform _turret;

    private ParticleSystem _shootEffect;

    private bool _isAttackable = true;

    private void Awake()
    {
        _shootEffect = GetComponentInChildren<ParticleSystem>();
    }

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

            _turret.transform.rotation = Quaternion.Euler(new Vector3(_turret.transform.rotation.x,
                (_targetEnemy.transform.position.x - _turret.transform.position.x) * 15f,
                _turret.transform.rotation.z));

            var bullet = Instantiate(_bullet, _shootPoint.position, Quaternion.identity);

            bullet.GetComponent<TowerBullet>().Initialize(_targetEnemy,_damage);

            _shootEffect.Play();

            AudioManager.instance.PlaySFX("NPCTower");

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
            _attackTimer = 0.2f;
        }
    }

}
