using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform hedefNokta; // Belirledi�iniz hedef noktan�n referans�

    private NavMeshAgent agent; // Navigation Mesh Agent bile�eni

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
        agent.SetDestination(hedefNokta.position); // Hedef noktaya do�ru ilerleme ba�lat�l�yor
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
