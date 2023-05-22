using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform hedefNokta; // Belirledi�iniz hedef noktan�n referans�

    private NavMeshAgent agent; // Navigation Mesh Agent bile�eni

    public bool isTargetable;
    private Animator animator;

    public Health Health;

    [SerializeField] private LayerMask _allyMask;

    private bool _isAllyTarget = false;

    private bool _isAttackable = true;

    public float saldiriMesafesi = 2f; // Sald�r� mesafesi
    public float saldiriZamani = 1f; // Sald�r� zaman aral���
    public float saldiriGucu = 10f; // Sald�r� g�c�
    private float sonSaldiriZamani; // Son sald�r� zaman�

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Health = GetComponent<Health>();
        hedefNokta = EnemyController.Instance.enemyTargetPoint;
        sonSaldiriZamani = Time.time; // Ba�lang��ta son sald�r� zaman�n� ayarla

        StartCoroutine(CO_CheckAlly());

        agent.SetDestination(hedefNokta.position); // Hedef noktaya do�ru ilerleme ba�lat�l�yor

    }

    private void Update()
    {
        if (agent.remainingDistance <= saldiriMesafesi && _isAttackable)
        {
            Sald�r();
            StartCoroutine(CO_AttackTimer());
            //sonSaldiriZamani = Time.time; // Sald�r� yap�ld���nda son sald�r� zaman�n� g�ncelle
        }
    }

    private IEnumerator CO_AttackTimer()
    {
        _isAttackable = false;
        yield return new WaitForSeconds(saldiriZamani);
        _isAttackable = true;
    }

    private IEnumerator CO_CheckAlly()
    {
        yield return new WaitForSeconds(.4f);
        CheckAlly();

        StartCoroutine(CO_CheckAlly());
    }

    private void CheckAlly()
    {
        if (_isAllyTarget)
        {
            return;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f,_allyMask);

        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("Ally alg�land�");
            hedefNokta = hitCollider.transform;
            _isAllyTarget = true;
            agent.SetDestination(hedefNokta.position); // Hedef noktaya do�ru ilerleme ba�lat�l�yor
            break;
        }
    }



    //Distance Check
    private Health targetHealth;
    private void Sald�r()
    {
        targetHealth = hedefNokta.GetComponent<Health>();
        if (targetHealth != null)
        {
            //hedefHealth.ReduceHealth(saldiriGucu);
            transform.LookAt(hedefNokta);
            animator.SetTrigger("Attack");
            Debug.Log("Hedefe sald�r� yap�ld�! Hasar: " + saldiriGucu);
        }
        else
        {
            Debug.Log("Hedefe Hasar: " + saldiriGucu);
        }
    }

    public void GiveDamage()
    {
        targetHealth.ReduceHealth(saldiriGucu);

    }


}
