using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public Transform _targetPoint; // Belirledi�iniz hedef noktan�n referans�

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

    [SerializeField] private List<ParticleSystem> _bloodEffects;
    [SerializeField] private List<ParticleSystem> _deathEffects;
    [SerializeField] private GameObject _starImage;

    private bool _isSpeacial;
    private void OnDisable()
    {
        if (_isSpeacial)
        {
            SkillUIController.Instance.OpenSpecialPowerUI();
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Health = GetComponent<Health>();
        _targetPoint = EnemyController.Instance.enemyTargetPoint;
        sonSaldiriZamani = Time.time; // Ba�lang��ta son sald�r� zaman�n� ayarla

        StartCoroutine(CO_CheckAlly());

        agent.SetDestination(_targetPoint.position); // Hedef noktaya do�ru ilerleme ba�lat�l�yor

        int random = Random.Range(0, 6);
        if (random == 3)
        {
            _starImage.SetActive(true);
            _isSpeacial = true;
        }
    }

    public void PlayBloodParticle()
    {
        foreach (var item in _bloodEffects)
        {
            item.Play();
        }
    }

    public void PlayDeathParticle()
    {
        _deathEffects[0].gameObject.transform.parent = null;
        foreach (var item in _deathEffects)
        {
            item.Play();
        }
    }

    private void Update()
    {
        if (_targetPoint == null)
        {
            MoveToCastle();
            return;
        }
        else
        {
            agent.SetDestination(_targetPoint.position);
        }

        if (agent.remainingDistance <= saldiriMesafesi && _isAttackable)
        {
            Debug.Log("xx");
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
        if (_isAllyTarget)
        {
            return;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f, _allyMask);

        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("Ally alg�land�");
            _targetPoint = hitCollider.gameObject.transform;
            _isAllyTarget = true;
            agent.SetDestination(_targetPoint.position); // Hedef noktaya do�ru ilerleme ba�lat�l�yor
            break;
        }
    }



    //Distance Check
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


        if (targetHealth != null)
        {
            targetHealth.ReduceHealth(saldiriGucu);
            if (targetHealth.GetHealth() <= 0)
            {
                _isAllyTarget = false;
                MoveToCastle();
            }
        }
        else
        {
            _isAllyTarget = false;
            MoveToCastle();
        }
    }

    private void MoveToCastle()
    {
        _targetPoint = EnemyController.Instance.enemyTargetPoint;
        if(_targetPoint != null)
        {
            agent.SetDestination(_targetPoint.position);

        }
    }


}
