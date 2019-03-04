using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ghostAgent : MonoBehaviour
{

    public Transform target;
    public Transform[] waypoints;
    public bool seePlayer = false;
    private int layermask = ~(1 << 9);
    private int curWaypoint = 0;
    private NavMeshAgent agent;
    public int mode;
    private float timer;
    private float timeSet = 0.5f;
    private RaycastHit hit;
    private int lastmode = 0;
    private float stall = 1f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (mode == 0) {
            // Chase
            agent.destination = target.position;
            timer = timeSet;
        } else {
            // Patrol
            agent.destination = waypoints[0].position;
        }
        agent.autoRepath = false;
    }

    void FixedUpdate()
    {
        if (mode == 0) {
            // Chase
            timer -= Time.deltaTime;
            if (timer < 0) {
                timer = timeSet;
                agent.destination = target.position;
            }
        } else if (mode == 1) {
            // Patrol
            Vector3 r = agent.destination - transform.position;
            r.y = 0;
            if (r.magnitude < .5) {
                curWaypoint++;
                if (curWaypoint >= waypoints.Length) {
                    curWaypoint = 0;
                }
                agent.destination = waypoints[curWaypoint].position;
            }
        } else {
            if ( stall == 0 )
            {
                stall = 1f;
                mode = lastmode;
            }
            else {
                stall = stall - Time.deltaTime;
            }
        }

        seePlayer = false;
        if (Physics.Raycast(transform.position, target.position - transform.position, out hit, Mathf.Infinity, layermask)) {
            if (hit.transform == target) {
                seePlayer = true;
            }
        }
    }

    void OnTriggerEnter( Collider other )
    {
        if ( other.tag.Equals("bullet") )
        {
            Debug.Log("shot");
            lastmode = mode;
            mode = 0;
        }
        
    }
}
