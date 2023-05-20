using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform hedefNokta; // Belirlediðiniz hedef noktanýn referansý

    private NavMeshAgent agent; // Navigation Mesh Agent bileþeni

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
        agent.SetDestination(hedefNokta.position); // Hedef noktaya doðru ilerleme baþlatýlýyor
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
