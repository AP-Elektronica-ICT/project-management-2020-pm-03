using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Patrol : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform EnemyGFX;
    public float DetectionRange;
    public EnemyAI EnemyAI;
    public float Speed;
    private float waittime;
    public float StartWaitTime;
    public Transform[] moveSpots;
    public int randomSpot;

    // Start is called before the first frame update
    void Start()
    {
        randomSpot = Random.Range(0, moveSpots.Length);
        EnemyAI.animator.SetFloat("Horizontal", 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, Speed * Time.deltaTime);
        // ga naar randomspot met AI
        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position )< 0.02f)
        {
            if (waittime <= 0)
            {
                int prevSpot = randomSpot;
                randomSpot = Random.Range(0, moveSpots.Length);
                waittime = StartWaitTime;
                if (randomSpot > prevSpot)
                {
                    EnemyAI.animator.SetFloat("Horizontal", 1);
                }
                else
                {
                    EnemyAI.animator.SetFloat("Horizontal", -1);
                }

            }
            else
            {
                waittime -= Time.deltaTime;
            }

        }
        if (Vector2.Distance(EnemyAI.target.position, rb.position) <= DetectionRange)
        {
            this.enabled = false;
            EnemyAI.enabled = true;
        }
        
    }
}
