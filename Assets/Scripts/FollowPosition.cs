using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    public Transform followTarget;

    Vector3 offset;

    void Start()
    {
        offset = transform.position - followTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = followTarget.position + offset;
    }
}
