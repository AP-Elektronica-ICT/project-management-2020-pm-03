using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyCombat : MonoBehaviour

{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask EnemyLayers;

    public float AttackRange = 1.5f;
    public int AttackDamage = 35;

    public EnemyAI movement;

    public float AttackRate = 2f;
    private float nextAttackTime = 0f;
    public int MaxHealth = 100;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
    }
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / AttackRate;
            }
        }
    }


    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] HitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, AttackRange, EnemyLayers);

        foreach (var player in HitPlayer)
        {
            Debug.Log("You hit " + player.name);
            player.GetComponent<EnemyCombat>().TakeDamage(AttackDamage);
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, AttackRange);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Enemy died!");
        animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        this.movement.enabled = false;

    }
}
