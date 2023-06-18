using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Rigidbody _cannonBall;
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private Transform _shootPoint;

    [SerializeField] private Transform _pivotPoint;

    [SerializeField] private Projectile _projectile;

    [SerializeField] private float _velocityMultiple;

    [SerializeField] private Image _shootTimeIndicator;

    [SerializeField] private float _shootTimer;

    private bool _isShootable = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isShootable)
        {
            if (!SkillFunctions.Instance.IsThreeBall)
            {
                SpawnBall();
            }
            else
            {
                StartCoroutine(CO_ThreeBall());
            }
            StartCoroutine(SetIsShootable());
        }

        Vector3 distance = _shootPoint.position - _pivotPoint.position;

        _pivotPoint.transform.rotation = Quaternion.Euler(distance.z * -3, distance.x * 10,_pivotPoint.transform.rotation.z);

        if (!_isShootable)
        {
            _shootTimeIndicator.fillAmount += 1.0f / _shootTimer * Time.deltaTime;
        }

    }

    private IEnumerator CO_ThreeBall()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnBall();
            yield return new WaitForSeconds(.25f);
        }
        SkillFunctions.Instance.IsThreeBall = false;
    }

    private void SpawnBall()
    {
        var ball = SpawnBall(_spawnPoint.position);
        MoveToTarget(ball.GetComponent<Rigidbody>(), new Vector3(_projectile.velocity.x, _projectile.velocity.y, _projectile.velocity.z * _velocityMultiple));
    }

    private GameObject SpawnBall(Vector3 pos)
    {
        var ball = Instantiate(_cannonBall.gameObject, pos, Quaternion.identity);
        return ball;
    }

    private void MoveToTarget(Rigidbody ball,Vector3 velocity)
    {
        ball.velocity = velocity;
    }

    private IEnumerator SetIsShootable()
    {
        _isShootable = false;
        //float time = 0;
        //while(time <= _shootTimer)
        //{
        //    time += Time.deltaTime;
        //    _shootTimeIndicator.fillAmount += (time / _shootTimer) * Time.deltaTime;
        //}
        _shootTimeIndicator.fillAmount = 0;
        yield return new WaitForSeconds(_shootTimer);
        _isShootable = true;
    }


}
