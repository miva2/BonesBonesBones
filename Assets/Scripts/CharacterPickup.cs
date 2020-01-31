using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPickup : MonoBehaviour
{
    public double pickupRange;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // When E is pressed we get the closest pickup object
        if (Input.GetKeyDown(KeyCode.E) && TryFindClosestPickup(out var pickup))
        {
            var diff = transform.position - pickup.transform.position;
            var distSq = diff.sqrMagnitude;
            // if it's within range to pick up we activate the pickup UI and disable player movement
            if (distSq < pickupRange * pickupRange)
            {
                DoPickup(pickup);
            }
        }
    }

    private void DoPickup(GameObject pickup)
    {
    }

    bool TryFindClosestPickup(out GameObject pickup)
    {
        var gos = GameObject.FindGameObjectsWithTag("pickup");
        pickup = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                pickup = go;
                distance = curDistance;
            }
        }

        return pickup != null;
    }
}
