using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ghostAgent : MonoBehaviour
{

    public Transform target;
    public Transform[] waypoints;
	public bool alwaysChase = false;
    public bool seePlayer = false;
    private int layermask = ~(1 << 8);
    public int currWaypoint = -1;
    private NavMeshAgent agent;
    public int mode;
    private float timer;
    private float timeSet = 4f;
    private RaycastHit hit;
    private int lastmode = 0;
    public float stall = 1f;

	private void setNextWaypoint(){ 
		try {
			currWaypoint = (currWaypoint + 1) % waypoints.Length;
			agent.SetDestination(waypoints[currWaypoint].transform.position);
		} catch {
			Debug.Log ( "Next Waypoint cannot be set due to array indexing issue or array is of length 0 " );
		}
	}

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
			setNextWaypoint ();
        }
        agent.autoRepath = false;
    }

    void FixedUpdate()
    {
		if (alwaysChase){
			if (stall > 1 ){
				if ( stall < 0 )
				{
					stall = 1f;
					mode = lastmode;
				}
				else {
					stall -= Time.deltaTime;
				}
			} else {
				agent.destination = target.position;
			}

		} else {
			if (Physics.Raycast(transform.position, target.position - transform.position, out hit, Mathf.Infinity, layermask)) {
				if (hit.transform == target) {
					seePlayer = true;
				} else {
					seePlayer = false;
				}		
			}

	        if (mode == 0) {
				agent.destination = target.position;
	            // Chase
				if (!seePlayer){
					if (timer < 0){
						setNextWaypoint();
						mode = 1;
					} else {
						timer -= Time.deltaTime;
					}
				} else {
					timer = timeSet;
				}
			} else if (mode == 1) {
	            // Patrol
				if (seePlayer){
					currWaypoint--;
					agent.destination = target.position;
					mode = 0;
				} else if (agent.remainingDistance < .5 && !agent.pathPending) {
					setNextWaypoint ();
				}
	        } else {
	            if ( stall < 0 )
	            {
	                stall = 1f;
	                mode = lastmode;
	            }
	            else {
	                stall -= Time.deltaTime;
	            }
	        }
		}     
    }


    void OnTriggerEnter( Collider other )
    {
        if ( other.tag.Equals("bullet") )
        {
            Debug.Log("shot");
			stall += .3f;
			if (mode < 2) {
            	lastmode = mode;
			}
			agent.destination = transform.position;
            mode = 2;
        }
        
    }
}
