using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform Target;
    public Rigidbody2D rb;
    public float DetectionRange;
    public EnemyAI EnemyAI;
    public float Speed;
    private float waittime;
    public float StartWaitTime;
    public Transform[] moveSpots;
    private int randomSpot;
    // Start is called before the first frame update
    void Start()
    {
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, Speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position )< 0.02f)
        {
            if (waittime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waittime = StartWaitTime;
            }
            else
            {
                waittime -= Time.deltaTime;
            }

        }
        if (Vector2.Distance(Target.position, rb.position) <= DetectionRange)
        {
            this.enabled = false;
            EnemyAI.enabled = true;
        }
    }
}
