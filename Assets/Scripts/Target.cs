using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Target : MonoBehaviour
{
    public float health = 100f;
    GameObject player;
    NavMeshAgent enemy;
    public Animator animator;
    

    public void TakeDamage(float amout)
    {
        health -= amout;

        if (health <= 0)
        {
            Die();
            return;
        }

        if (health <= 20)
        {
            animator.SetBool("IsDying", true);
            if (health != 0)
            {
                animator.SetBool("IsWalking", false);

                animator.SetBool("IsCrawling", true);
            }
            //Die();
        }
    }

    private void Die()
    {
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsDeath", true);
        Destroy(gameObject, 3f);
    }

    private void Start()
    {
        health = 100f;
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<NavMeshAgent>();
        animator.SetBool("IsWalking", true);

    }


    private void Update()
    {
        if (player != null)
        {
            enemy.destination = player.transform.position;

            if (enemy.remainingDistance <= 4 && enemy.remainingDistance >= 2)
                animator.SetBool("IsAttacking", true);
            else
                animator.SetBool("IsAttacking", false);

            if (enemy.remainingDistance != Mathf.Infinity && enemy.remainingDistance <= 2)
            {
                animator.SetBool("IsBitting", true);
            }
            else
            {
                animator.SetBool("IsBitting", false);
            }

        }
    }
}
