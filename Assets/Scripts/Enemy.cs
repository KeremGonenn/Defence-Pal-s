using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform hedefNokta; // Belirlediğiniz hedef noktanın referansı

    private NavMeshAgent agent; // Navigation Mesh Agent bileşeni

    public bool isTargetable;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        hedefNokta = EnemyController.Instance.enemyTargetPoint;
    }

    private void Update()
    {
        agent.SetDestination(hedefNokta.position); // Hedef noktaya doğru ilerleme başlatılıyor
    }
}
