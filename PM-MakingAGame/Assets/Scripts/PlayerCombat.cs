using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{

    
    public Animator animator;

    public GameObject Boss;
    
    public PlayerMovement movement;

    public DifficultySetting df;


    public int MaxHealth;
    private int currentHealth;

    public Transform attackPoint;
    public LayerMask EnemyLayers;

    public float AttackRange = 1.5f;
    public int AttackDamage = 35;

    public float AttackRate = 2f;
    private float nextAttackTime = 0f;

    private float nextBlockTime = 0f;
    public float BlockRate = 2f;
    private bool Blockbool = false;


    public HealthBar healthbar;

    private WaitForSeconds regenTick = new WaitForSeconds(0.2f);
    private Coroutine regen;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("level1");
        FindObjectOfType<AudioManager>().Play("HeroRun");

        SetDiff();
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
        if (Time.time >= nextBlockTime)
        {
            Blockbool = false;
            if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.LeftShift))
            {
                Block();
                nextBlockTime = Time.time + 1f / BlockRate;
            }
        }
    }
    void Block()
    {
        animator.SetTrigger("Block");
        Blockbool = true;
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        FindObjectOfType<AudioManager>().Play("HeroAttack");
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, AttackRange, EnemyLayers);

        foreach (var enemy in HitEnemies)
        {
                //Debug.Log("You hit " + enemy.name);
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
        if (Blockbool == false)
        {
            currentHealth -= damage;
            animator.SetTrigger("Hurt");
            FindObjectOfType<AudioManager>().Play("HeroHit");
            healthbar.Sethealth(currentHealth);
        }
        
        if (regen != null)
        {
            StopCoroutine(regen);
        }
        regen = StartCoroutine(HealthRegen());
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Debug.Log("U died!");
        animator.SetBool("IsDead", true);
        FindObjectOfType<AudioManager>().Play("HeroDeath");
        Invoke("StartDeathScreen", 1.5f);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        this.movement.enabled= false;
    }

    private void StartDeathScreen()
    {
        SceneManager.LoadScene("Death Screen");
    }

    private void SetDiff()
    {
        int health = 0;
        int damage = 0;
        switch (DifficultySetting.difficultyMode)
        {
            case Diff.Ez:
                health = 200;
                damage = 50;
                break;
            case Diff.Norm:
                health = 150;
                damage = 40;
                break;
            case Diff.Hard:
                health = 100;
                damage = 35;
                break;

            default:
                break;
        }

        MaxHealth = health;
        currentHealth = MaxHealth;
        healthbar.SetMaxHealth(MaxHealth);
        AttackDamage = damage;
    }

    private IEnumerator HealthRegen()
    {
        switch (DifficultySetting.difficultyMode)
        {
            case Diff.Ez:
                yield return new WaitForSeconds(5);
                while (currentHealth < MaxHealth)
                {
                    currentHealth += 1;
                    healthbar.Sethealth(currentHealth);
                    yield return regenTick;
                }
                break;

            case Diff.Norm:
                yield return new WaitForSeconds(10);
                while (currentHealth < MaxHealth)
                {
                    currentHealth += 1;
                    healthbar.Sethealth(currentHealth);
                    yield return regenTick;
                }
                break;

            case Diff.Hard:
                break;

            default:
                break;
        }
        
        
    }

   







}