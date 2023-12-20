using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public GameObject waypoint1;
    public GameObject waypoint2;
    public GameObject player;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;
    public float chaseRange = 5f; // Jarak di mana musuh mulai mengejar pemain

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = waypoint2.transform;
        anim.SetBool("isWalk", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerVisible())
        {
            ChasePlayer();
        }
        else
        {
            MoveBetweenWaypoints();
        }
    }

    void MoveBetweenWaypoints()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == waypoint2.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == waypoint2.transform)
        {
            if (player.transform.position.x > transform.position.x)
            {
                IsVisible = true;
            }
            else { IsVisible = false; }
            flip();
            currentPoint = waypoint1.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == waypoint1.transform)
        {
            if (player.transform.position.x < transform.position.x)
            {
                IsVisible = true;
            }
            else { IsVisible = false; }
            flip();
            currentPoint = waypoint2.transform;
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);

        if (direction.x > 0 && transform.localScale.x < 0)
        {
            flip();
        }
        else if (direction.x < 0 && transform.localScale.x > 0)
        {
            flip();
        }
    }

    private bool IsVisible;

    bool IsPlayerVisible()
    {
        // Periksa apakah pemain berada dalam jarak chaseRange
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        return distanceToPlayer < chaseRange;
    }

    void flip()
    {
        Vector3 localscale = transform.localScale;
        localscale.x *= -1;
        transform.localScale = localscale;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(waypoint1.transform.position, 0.5f);
        Gizmos.DrawWireSphere(waypoint2.transform.position, 0.5f);
        Gizmos.DrawLine(waypoint1.transform.position, waypoint2.transform.position);
    }
}
