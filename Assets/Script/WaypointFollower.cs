using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public GameObject waypoint1;
    public GameObject waypoint2;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;

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
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == waypoint2.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == waypoint2.transform)
        {
            flip();
            currentPoint = waypoint1.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == waypoint1.transform)
        {
            flip();
            currentPoint = waypoint2.transform;
        }
    }

    private void flip()
    {
        Vector3 localscale = transform.localScale;
        localscale.x *= -1;
        transform.localScale = localscale;  
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(waypoint1.transform.position, 0.5f);
        Gizmos.DrawWireSphere(waypoint2.transform.position, 0.5f);
        Gizmos.DrawLine(waypoint1.transform.position, waypoint2.transform.position);
    }
}
