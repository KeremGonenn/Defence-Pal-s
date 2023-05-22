using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttack : MonoBehaviour
{
    public float detectionRadius = 10f; // Düþmaný tespit etmek için kullanýlan algýlama yarýçapý
    public float attackRange = 2f; // Saldýrý mesafesi

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
                        // Düþman tespit edildi, hedef olarak belirle
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
                    // Saldýrý mesafesine gelindi, saldýrý animasyonunu çalýþtýr
                    Attack();
                }
            }
        }
    }

    private void Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");

        // Saldýrý animasyonunun süresini alarak animasyonun tamamlanmasýný bekler
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
