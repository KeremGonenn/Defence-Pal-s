using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform hedefNokta; // Belirledi�iniz hedef noktan�n referans�

    private NavMeshAgent agent; // Navigation Mesh Agent bile�eni

    public bool isTargetable;
    private Animator animator;
    private Health _health;

    public float saldiriMesafesi = 2f; // Sald�r� mesafesi
    public float saldiriZamani = 1f; // Sald�r� zaman aral���
    public float saldiriGucu = 10f; // Sald�r� g�c�
    private float sonSaldiriZamani; // Son sald�r� zaman�

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        _health = GetComponent<Health>();
        hedefNokta = EnemyController.Instance.enemyTargetPoint;
        sonSaldiriZamani = Time.time; // Ba�lang��ta son sald�r� zaman�n� ayarla
    }

    private void Update()
    {
        agent.SetDestination(hedefNokta.position); // Hedef noktaya do�ru ilerleme ba�lat�l�yor
        if (agent.remainingDistance <= saldiriMesafesi && Time.time >= sonSaldiriZamani + saldiriZamani)
        {
            Sald�r();
            sonSaldiriZamani = Time.time; // Sald�r� yap�ld���nda son sald�r� zaman�n� g�ncelle
        }

    }

    private void Sald�r()
    {
        // Hedefe hasar verme i�lemleri burada ger�ekle�tirilebilir
        // �rne�in, hedefin bir Health bile�eni varsa, onun ReduceHealth() metodu kullan�labilir
        Health hedefHealth = hedefNokta.GetComponent<Health>();
        if (hedefHealth != null)
        {
            //hedefHealth.ReduceHealth(saldiriGucu);
            Debug.Log("Hedefe sald�r� yap�ld�! Hasar: " + saldiriGucu);
        }
        else
        {
            animator.SetTrigger("Attack");
            Debug.Log("Hedefe Hasar: " + saldiriGucu);
        }
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
