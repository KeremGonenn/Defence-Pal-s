using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierAttack : MonoBehaviour
{
    public Transform _targetPoint; // Belirledi�iniz hedef noktan�n referans�

    private NavMeshAgent agent; // Navigation Mesh Agent bile�eni

    public bool isTargetable;
    private Animator animator;

    public Health Health;

    public PatrolPoint currentPatrolPoint;

    [SerializeField] private LayerMask _enemyMask;

    private bool _isEnemyTarget = false;

    private bool _isAttackable = true;

    public float saldiriMesafesi = 1f; // Sald�r� mesafesi
    public float saldiriZamani = 1f; // Sald�r� zaman aral���
    public float saldiriGucu = 10f; // Sald�r� g�c�
    private float sonSaldiriZamani; // Son sald�r� zaman�

    private void Start()
    {
        //_targetPoint = AllysPatrol.Instance.GetEmptyPoint(this);
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Health = GetComponent<Health>();
        //sonSaldiriZamani = Time.time; // Ba�lang��ta son sald�r� zaman�n� ayarla

        StartCoroutine(CO_CheckAlly());

        //agent.SetDestination(_targetPoint.position); // Hedef noktaya do�ru ilerleme ba�lat�l�yor

    }

    private void Update()
    {
        if(_targetPoint == null)
        {
            return;
        }
        else
        {
            agent.SetDestination(_targetPoint.position);

        }


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
        yield return new WaitForSeconds(.1f);
        CheckAlly();

        StartCoroutine(CO_CheckAlly());
    }

    private void CheckAlly()
    {
        if (_isEnemyTarget)
        {
            return;
        }

        HandleOverlapSphere();

        //if (!_isEnemyTarget)
        //{
        //    bool isPatrol;
        //    int value = Random.Range(0, 2);
        //    isPatrol =  value == 0 ? true : false;

        //    if (isPatrol)
        //    {
        //        _targetPoint = AllysPatrol.Instance.GetEmptyPoint(this);
        //    }

        //}
    }

    private void HandleOverlapSphere()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f, _enemyMask);

        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("Enemy alg�land�");
            _targetPoint = hitCollider.gameObject.transform;
            _isEnemyTarget = true;
            currentPatrolPoint.IsEmpty = true;
            agent.SetDestination(_targetPoint.position); // Hedef noktaya do�ru ilerleme ba�lat�l�yor
            break;
        }
    }

    private Health targetHealth;
    private void Sald�r()
    {
        targetHealth = _targetPoint.GetComponent<Health>();
        if (targetHealth != null)
        {
            //hedefHealth.ReduceHealth(saldiriGucu);
            transform.LookAt(_targetPoint);
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
        if(targetHealth != null)
        {
            targetHealth.ReduceHealth(saldiriGucu);
            if(targetHealth.GetHealth() <= 0)
            {
                _isEnemyTarget = false;
            }
        }
        else
        {
            _isEnemyTarget = false;
        }
    }
}
