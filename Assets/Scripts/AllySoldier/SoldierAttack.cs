using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttack : MonoBehaviour
{
    public float detectionRadius = 10f; // D��man� tespit etmek i�in kullan�lan alg�lama yar��ap�
    public float attackRange = 2f; // Sald�r� mesafesi

    private Animator animator;
    private bool isAttacking = false;
    private Transform target;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isAttacking)
        {
            if (target == null)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("Enemy"))
                    {
                        // D��man tespit edildi, hedef olarak belirle
                        target = collider.transform;
                        break;
                    }
                }
            }
            else
            {
                float distance = Vector3.Distance(transform.position, target.position);

                if (distance <= attackRange)
                {
                    // Sald�r� mesafesine gelindi, sald�r� animasyonunu �al��t�r
                    Attack();
                }
            }
        }
    }

    private void Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");

        // Sald�r� animasyonunun s�resini alarak animasyonun tamamlanmas�n� bekler
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "Attack")
            {
                StartCoroutine(ResetAttackState(clip.length));
                break;
            }
        }
    }

    private IEnumerator ResetAttackState(float duration)
    {
        yield return new WaitForSeconds(duration);

        isAttacking = false;
    }
}
