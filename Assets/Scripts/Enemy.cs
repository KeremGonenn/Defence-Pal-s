using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform hedefNokta; // Belirlediðiniz hedef noktanýn referansý

    private NavMeshAgent agent; // Navigation Mesh Agent bileþeni

    public bool isTargetable;
    private Animator animator;
    private Health _health;

    public float saldiriMesafesi = 2f; // Saldýrý mesafesi
    public float saldiriZamani = 1f; // Saldýrý zaman aralýðý
    public float saldiriGucu = 10f; // Saldýrý gücü
    private float sonSaldiriZamani; // Son saldýrý zamaný

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        _health = GetComponent<Health>();
        hedefNokta = EnemyController.Instance.enemyTargetPoint;
        sonSaldiriZamani = Time.time; // Baþlangýçta son saldýrý zamanýný ayarla
    }

    private void Update()
    {
        agent.SetDestination(hedefNokta.position); // Hedef noktaya doðru ilerleme baþlatýlýyor
        if (agent.remainingDistance <= saldiriMesafesi && Time.time >= sonSaldiriZamani + saldiriZamani)
        {
            Saldýr();
            sonSaldiriZamani = Time.time; // Saldýrý yapýldýðýnda son saldýrý zamanýný güncelle
        }

    }

    private void Saldýr()
    {
        // Hedefe hasar verme iþlemleri burada gerçekleþtirilebilir
        // Örneðin, hedefin bir Health bileþeni varsa, onun ReduceHealth() metodu kullanýlabilir
        Health hedefHealth = hedefNokta.GetComponent<Health>();
        if (hedefHealth != null)
        {
            //hedefHealth.ReduceHealth(saldiriGucu);
            Debug.Log("Hedefe saldýrý yapýldý! Hasar: " + saldiriGucu);
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
