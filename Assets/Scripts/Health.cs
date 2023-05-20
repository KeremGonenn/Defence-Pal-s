using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health;
    public float HealthValue { get { return _health; } set {HealthValue = _health; } } 
}
