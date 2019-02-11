using UnityEngine;
using Pathfinding;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent(typeof (Seeker))]
public class EnemyAI : MonoBehaviour {

    //waht to chase
    public Transform target;

    //how path updates a second
    public float updateRate = 2f;

    //caching
    private Seeker seeker;
    private Rigidbody2D rb;

    //the calculated path
    public Path path;

    //the AI speed per second
    public float speed = 300f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;

    //max distance from ai to waypoint for it to continue to next waypoint
    public float nextWaypointDist = 3;

    //the waypoint we are currently moving towards
    private int currentWaypoint = 0;

    private void Start()
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if(target == null)
        {
            Debug.Log("No target available");
            return;
        }

        //start a new path to the target position, return result to the OnPathComplete function
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(updatePath());
    }

    public void OnPathComplete(Path p)
    {
        //Debug.Log("We got a path");
        if (p.error)
        {
            Debug.Log("Error: " + p.error);
        }
        else
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    IEnumerator updatePath()
    {
        if(target == null)
        {
            Debug.Log("Still no target available");
            yield return null;
        }
        else
        {
            //start a new path to the target position, return result to the OnPathComplete function
            seeker.StartPath(transform.position, target.position, OnPathComplete);

            yield return new WaitForSeconds(1/updateRate);
            StartCoroutine(updatePath());
        }
    }

    private void FixedUpdate()
    {
        if(target == null)
        {
            Debug.Log("No Target Dummy");
            return;
        }
        else
        {
            if(path == null)
            {
                return;
            }
            
            if(currentWaypoint >= path.vectorPath.Count)
            {
                if (pathIsEnded)
                {
                    return;
                }
                //Debug.Log("End of path reached");
                pathIsEnded = true;
                return;
            }
            pathIsEnded = false;

            //direction to next waypoint
            Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            dir *= speed * Time.fixedDeltaTime;

            //move AI
            rb.AddForce(dir, fMode);

            float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);

            if (dist < nextWaypointDist)
            {
                currentWaypoint++;
                return;
            }
        }
    }
}
