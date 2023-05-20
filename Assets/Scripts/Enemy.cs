using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform hedefNokta; // Belirlediðiniz hedef noktanýn referansý

    private NavMeshAgent agent; // Navigation Mesh Agent bileþeni

    public bool isTargetable;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        hedefNokta = EnemyController.Instance.enemyTargetPoint;
    }

    private void Update()
    {
        agent.SetDestination(hedefNokta.position); // Hedef noktaya doðru ilerleme baþlatýlýyor
    }
}
