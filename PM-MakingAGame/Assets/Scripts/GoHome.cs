using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHome : MonoBehaviour
{
    public Transform homeSpot;

    public EnemyAI enemyAI;
    public Patrol patrol;

    public Rigidbody2D rb;
    public float nextWaypointDistance = 3f;
    public Vector2 force;
    public float speed = 200f;
    public Animator animator;
    public Transform attackPoint;

    private Seeker seeker;
    private Path path;
    private int currentWaypoint = 0;


    // Start is called before the first frame update
    void Start()
    {
        // Search for the desired components
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        // Repeatfunction for updated pathfinding
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        this.enabled = false;
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, homeSpot.position, OnPathComplete);

    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Follow path
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        if (rb.velocity.x >= 0.01f)
        {
            animator.SetFloat("Horizontal", 1);
            //enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
            attackPoint.localPosition = new Vector3(1, 0);
        }
        if (rb.velocity.x <= 0.01f)
        {
            animator.SetFloat("Horizontal", -1);
            //enemyGFX.localScale = new Vector3(1f, 1f, 1f);
            attackPoint.localPosition = new Vector3(-1, 0);
        }
    }
}
