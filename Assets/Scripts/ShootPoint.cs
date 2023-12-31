using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPoint : MonoBehaviour
{
    private float _horizontal;
    private float _vertical;
    [SerializeField] private float _speed;
    [SerializeField] private FixedJoystick _joystick;

    private void Update()
    {
        _horizontal = _joystick.Horizontal;
        _vertical = _joystick.Vertical;

        transform.position += new Vector3(_horizontal, 0, _vertical) * Time.deltaTime * _speed;

    }


}
