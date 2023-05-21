using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Rigidbody _cannonBall;
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private Projectile _projectile;

    [SerializeField] private float _velocityMultiple;

    private bool _isShootable = true;

    [SerializeField] private float _shootTimer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isShootable)
        {
            var ball = SpawnBall(_spawnPoint.position);

            MoveToTarget(ball.GetComponent<Rigidbody>(),new Vector3(_projectile.velocity.x, _projectile.velocity.y, _projectile.velocity.z * _velocityMultiple));

            StartCoroutine(SetIsShootable());
        }
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
        yield return new WaitForSeconds(_shootTimer);
        _isShootable = true;
    }


}
