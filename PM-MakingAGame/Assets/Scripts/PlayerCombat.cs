using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public PlayerMovement movement;

    public int MaxHealth = 100;
    private int currentHealth;

    public Transform attackPoint;
    public LayerMask EnemyLayers;

    public float AttackRange = 1.5f;
    public int AttackDamage = 35;

    public float AttackRate = 2f;
    private float nextAttackTime = 0f;

    void Start()
    {
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time>=nextAttackTime)
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

        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, AttackRange, EnemyLayers);

        foreach (var enemy in HitEnemies)
        {
            Debug.Log("You hit " + enemy.name);
            enemy.GetComponent<EnemyCombat>().TakeDamage(AttackDamage);
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
        Debug.Log("U died!");
        animator.SetBool("IsDead", true);

        Invoke("StartDeathScreen", 1.5f);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        this.movement.enabled= false;
    }

    private void StartDeathScreen()
    {
        SceneManager.LoadScene("Death Screen");
    }
}