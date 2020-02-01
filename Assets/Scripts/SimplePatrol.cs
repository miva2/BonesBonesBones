using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimplePatrol : MonoBehaviour
{

    public GameObject[] patrolPoints;
    public float moveSpeed;
    public float destinationReachedRadius = 0.4f;

    private NavMeshAgent navMeshAgent;

    private Vector3 nextDestination;
    private int patrolPointIndex = 0;
    private bool destinationReached = false;



    // Start is called before the first frame update
    void Start()
    {
        nextDestination = patrolPoints[patrolPointIndex].transform.position;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = true;
        navMeshAgent.SetDestination(nextDestination);
    }

    // Update is called once per frame
    void Update()
    {

        if (IsDestinationReached())
        {
            nextDestination = GetNextDestination();
            navMeshAgent.SetDestination(nextDestination);
            //Debug.Log("setting new destination: " + nextDestination);
        }

    }

    Vector3 GetNextDestination()
    {
        int amount = patrolPoints.Length;

        if(patrolPointIndex < amount-1)
        {
            patrolPointIndex++;
        }
        else
        {
           patrolPointIndex = 0;
        }

        return patrolPoints[patrolPointIndex].transform.position;
    }

    bool IsDestinationReached()
    {
        var dist = Vector3.Distance(transform.position, nextDestination);
        return dist <= destinationReachedRadius;
    }
}
