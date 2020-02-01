using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePatrol : MonoBehaviour
{

    public GameObject[] patrolPoints;
    public float moveSpeed;


    private Vector3 nextDestination;
    private int patrolPointIndex = 0;
    private bool destinationReached = false;

    private float destinationReachedRadius = 0.1f;



    // Start is called before the first frame update
    void Start()
    {
        nextDestination = patrolPoints[patrolPointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextDestination, moveSpeed);

        destinationReached = Vector3.Distance(transform.position, nextDestination) <= destinationReachedRadius;

        if (destinationReached)
        {
            UpdatePatrolPointIndex();
            nextDestination = patrolPoints[patrolPointIndex].transform.position;
        }

        transform.LookAt(nextDestination);
    }

    void UpdatePatrolPointIndex()
    {
        int amount = patrolPoints.Length;

        if(patrolPointIndex <= amount)
        {
            patrolPointIndex++;
        }
        else
        {
           patrolPointIndex = 0;
        }
    }
}
