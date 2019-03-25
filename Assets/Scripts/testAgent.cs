using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class testAgent : MonoBehaviour
{

    public Transform goal;

    public float timer;
    public float timeSet;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position; 
        timer = timeSet;
    }

    void Update() {
        timer -= Time.deltaTime;
        if (timer < 0) {
            timer = timeSet;
            agent.destination = goal.position;
        }
    }


}
