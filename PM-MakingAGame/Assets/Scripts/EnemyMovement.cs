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
    public Seeker seeker;
    public Seeker seekerForHome;

    // Patrol
    public float GuardingSpeed;
    public float DetectionRange;
    public Vector2[] MoveSpots;

    private Vector2 home;
    private float startWaitTime = 0;
    private float waittime;
    private int randomSpot;

    void Start()
    {
        // Search for components needed for EnemyAI
        Rb = GetComponent<Rigidbody2D>();

        // Seach for components needed for Patrol
        randomSpot = Random.Range(0, MoveSpots.Length);
        Animator.SetFloat("Horizontal", 1);
        home = MoveSpots[0];

        // Start repeating the updatefunction for active pathfinding
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        InvokeRepeating("UpdatePathForHome", 0f, 0.5f);
    }

    // Pathfinding
    private void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(Rb.position, Target.position, OnPathComplete);
    }

    private void UpdatePathForHome()
    {
        if (seekerForHome.IsDone())
            seekerForHome.StartPath(Rb.position, home, OnPathComplete);
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
        Debug.Log($"Target: {Target.position} Home: {home}");
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
            if (Rb.position.x >= MoveSpots[randomSpot].x)
            {
                Animator.SetFloat("Horizontal", -1);
            }
            else if (Rb.position.x <= MoveSpots[randomSpot].y)
            {
                Animator.SetFloat("Horizontal", 1);
            }

            Engaging = false;
            Patrolling = true;
        }
    }

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, MoveSpots[randomSpot], GuardingSpeed * Time.deltaTime);

        // ga naar randomspot met AI
        if (Vector2.Distance(transform.position, MoveSpots[randomSpot]) < 0.02f)
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
