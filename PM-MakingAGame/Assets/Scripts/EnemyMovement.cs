using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    // General
    public Transform EnemyGFX;
    public Animator Animator;

    private Rigidbody2D Rb;
    private bool Patrolling = true;
    private bool Engaging = false;
    // EnemyAI
    public Transform Target;
    public float EngagingSpeed = 200f;
    public float NextWaypointDistance = 3f;
    public Vector2 Force;
    public Transform AttackPoint;

    private Path path;
    private int currentWaypoint = 0;
    private Seeker seeker;

    // Patrol
    public float GuardingSpeed;
    public float DetectionRange;
    public Transform[] MoveSpots;
    private float startWaitTime = 0;
    private float waittime;
    private int randomSpot;

    void Start()
    {
        // Search for components needed for EnemyAI
        seeker = GetComponent<Seeker>();
        Rb = GetComponent<Rigidbody2D>();

        // Seach for components needed for Patrol
        randomSpot = Random.Range(0, MoveSpots.Length);
        Animator.SetFloat("Horizontal", 1);

        // Start repeating the updatefunction for active pathfinding
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    // Pathfinding
    private void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(Rb.position, Target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    void Update()
    {
        Check();

        if (Engaging)
            EnemyAI();
        if (Patrolling)
            Patrol();
    }

    private void Check()
    {
        if (Vector2.Distance(Target.position, Rb.position) <= DetectionRange)
        {
            Patrolling = false;
            Engaging = true;
        }
    }

    private void EnemyAI()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - Rb.position).normalized;
        Force = direction * EngagingSpeed * Time.deltaTime;

        Rb.AddForce(Force);

        float distance = Vector2.Distance(Rb.position, path.vectorPath[currentWaypoint]);

        if (distance < NextWaypointDistance)
        {
            currentWaypoint++;
        }
        if (Rb.velocity.x >= 0.01f)
        {
            Animator.SetFloat("Horizontal", 1);
            //enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
            AttackPoint.localPosition = new Vector3(1, 0);
        }
        if (Rb.velocity.x <= 0.01f)
        {
            Animator.SetFloat("Horizontal", -1);
            //enemyGFX.localScale = new Vector3(1f, 1f, 1f);
            AttackPoint.localPosition = new Vector3(-1, 0);
        }

        if (Vector2.Distance(Target.position, Rb.position) >= DetectionRange)
        {
            if (Rb.position.x >= MoveSpots[randomSpot].position.x)
            {
                Animator.SetFloat("Horizontal", -1);
            }
            else if (Rb.position.x <= MoveSpots[randomSpot].position.x)
            {
                Animator.SetFloat("Horizontal", 1);
            }

            Engaging = false;
            Patrolling = true;
        }
    }

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, MoveSpots[randomSpot].position, GuardingSpeed * Time.deltaTime);
        // ga naar randomspot met AI
        if (Vector2.Distance(transform.position, MoveSpots[randomSpot].position) < 0.02f)
        {
            if (waittime <= 0)
            {
                int prevSpot = randomSpot;
                randomSpot = Random.Range(0, MoveSpots.Length);
                waittime = startWaitTime;
                if (randomSpot > prevSpot)
                {
                    Animator.SetFloat("Horizontal", 1);
                }
                else
                {
                    Animator.SetFloat("Horizontal", -1);
                }

            }
            else
            {
                waittime -= Time.deltaTime;
            }

        }
    }
}
