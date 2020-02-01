using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePatrol : MonoBehaviour
{

    public GameObject[] patrolPoints;

    private Vector3 nextDestination;


    // Start is called before the first frame update
    void Start()
    {
        nextDestination = patrolPoints[0].transform.position;
    }

// Update is called once per frame
void Update()
    {
        //transform.position = Vector3.MoveTowards
    }
}
