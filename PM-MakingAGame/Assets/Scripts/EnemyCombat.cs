
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCombat : MonoBehaviour

{
    Transform Player;
    Rigidbody2D rb;
    public Animator animator;
    public Transform attackPoint;
    public LayerMask EnemyLayers;

    public float AttackRange = 1.5f;
    public int AttackDamage = 10;

    public float AttackRate = 2f;
    private float nextAttackTime = 0f;

    public EnemyAI movement;

    public HealthBar healthbar;

    public int MaxHealth = 100;
    private int currentHealth;

    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        healthbar.SetMaxHealth(MaxHealth);
        
    }

    void Update()
    {   
        if (Vector2.Distance(Player.position,rb.position)<= AttackRange)
        {   
            if (Time.time>nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + AttackRate;
            }
        }   
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        if (animator.name == "BanditGFX" || animator.name == "SmallSkeletonGFX")
        {
            FindObjectOfType<AudioManager>().Play("BigEnemyAttack");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("AttackEnemy");

        }
        

        Collider2D[] HitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, AttackRange, EnemyLayers);

        foreach (var player in HitPlayer)
        {
            //Debug.Log("U got hit ");
            player.GetComponent<PlayerCombat>().TakeDamage(AttackDamage);
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
        
        animator.SetTrigger("Attack");
        if (animator.name == "BanditGFX" || animator.name == "SmallSkeletonGFX")
        {
            FindObjectOfType<AudioManager>().Play("BigEnemyHit");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("HitEnemy");

        }
        animator.SetTrigger("Hurt");
        healthbar.Sethealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();  
        }
    }

    void Die()
    {
        //Debug.Log("Enemy died!");
        //FindObjectOfType<AudioManager>().Play("DeathEnemy");
        animator.SetTrigger("Attack");
        if (animator.name == "BanditGFX" || animator.name == "SmallSkeletonGFX")
        {
            FindObjectOfType<AudioManager>().Play("BigEnemyDeath");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("DeathEnemy");

        }
        animator.SetBool("IsDead", true);
        if (this.MaxHealth<=50)
        {
            Scorescript.ScoreValue += 1;
        }
        else if (this.MaxHealth<=100)
        {
            Scorescript.ScoreValue += 2;
        }
        else
        {
            Scorescript.ScoreValue += 3;
        }
        this.healthbar.Death();
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        this.movement.enabled = false;
        movement.rb.simulated = false;
    }
    
}
