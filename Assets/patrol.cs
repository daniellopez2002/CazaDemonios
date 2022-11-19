using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class patrol : MonoBehaviour
{
    public Transform[] points;
    public bool lookPlayer;
    private int destPoint = 0;
    public NavMeshAgent agent;
    [SerializeField] Transform player;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        //if (points.Length == 0)
        //    return;

        if (!lookPlayer)
        {
            Debug.Log("No looking");
            // Set the agent to go to the currently selected destination.
            agent.destination = points[destPoint].position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            destPoint = (destPoint + 1) % points.Length;
        }
        else
        {
            //agent.Stop();
            Debug.Log("looking");
            // Set the agent to go to the currently selected destination.
            //agent.destination = player.position;
            //agent.Resume();

        }

        
    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
}
