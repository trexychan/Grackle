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
    private float timeSet = 0.2f;
    private RaycastHit hit;

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
    }

    void Update()
    {
        if (mode == 0) {
            // Chase
            timer -= Time.deltaTime;
            if (timer < 0) {
                timer = timeSet;
                agent.destination = target.position;
            }
        } else {
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
        }

        seePlayer = false;
        if (Physics.Raycast(transform.position, target.position - transform.position, out hit, Mathf.Infinity, layermask)) {
            if (hit.transform == target) {
                seePlayer = true;
            }
        }
    }
}
