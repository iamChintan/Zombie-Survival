using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private GameObject player;
    [SerializeField] private NavMeshAgent enemy;
    [SerializeField] private Animator animator;
    bool isZombieDied = false;

    /// <summary>
    /// to damage the enemy, and play animation based on the health percentage
    /// </summary>
    /// <param name="amout"> amount defines the scale of the health, power is depends on the guns</param>
    public void TakeDamage(float amout)
    {
        health -= amout;

        if (health <= 0 && !isZombieDied)
        {
            isZombieDied = !isZombieDied;
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
        }
    }

    private void Die()
    {
        GameManager.Instance.UpdateScore();
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsDeath", true);
        ZombieGenerater.Instance.enemyCount--;
        ZombieGenerater.Instance.StartCoroutine(ZombieGenerater.Instance.SpawnEnemy());
        Destroy(gameObject, 3f);
    }

    public void BlastDie()
    {
        Debug.Log("BlastDie");
        GameManager.Instance.UpdateScore();
        animator.SetBool("IsBlastDying", true);
        ZombieGenerater.Instance.enemyCount--;
        ZombieGenerater.Instance.StartCoroutine(ZombieGenerater.Instance.SpawnEnemy());
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
                animator.SetBool("IsBitting", true);
            else
                animator.SetBool("IsBitting", false);
        }
    }
}
