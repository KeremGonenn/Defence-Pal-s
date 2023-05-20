using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform hedefNokta; // Belirlediğiniz hedef noktanın referansı

    private NavMeshAgent agent; // Navigation Mesh Agent bileşeni

    public bool isTargetable;

    private Health _health;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _health = GetComponent<Health>();
        hedefNokta = EnemyController.Instance.enemyTargetPoint;
    }

    private void Update()
    {
        agent.SetDestination(hedefNokta.position); // Hedef noktaya doğru ilerleme başlatılıyor
    }

    public float GetHealth()
    {
        return _health.HealthValue;
    }

    public void ReduceHealth(float value)
    {
        _health.HealthValue -= value;

    }
}
