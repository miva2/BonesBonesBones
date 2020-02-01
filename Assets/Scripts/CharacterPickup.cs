using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPickup : MonoBehaviour
{
    GameObject currentPickup;

    public GameObject repairCanvas;
    public double pickupRange;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // check if pickup left our range
        if (currentPickup != null && !InPickupRange(currentPickup))
        {
            DisableOutline(currentPickup);
            currentPickup = null;
        }

        // update the closest pickup
        if (TryFindClosestPickup(out var pickup) && pickup != currentPickup)
        {
            if (currentPickup != null)
            {
                DisableOutline(currentPickup);
                currentPickup = null;
            }

            // if it's within range we change current pickup and highlight it
            if (InPickupRange(pickup))
            {
                EnableOutline(pickup);
                currentPickup = pickup;
            }
        }

        // if we're not picking up a bone (UI is inactive) and we press the E key, do pickup
        if (!repairCanvas.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            // if a pickup is within range we activate the pickup UI
            if (currentPickup != null)
            {
                DoPickup(pickup);
            }
        }
    }

    private void DisableOutline(GameObject pickup)
    {
        pickup.layer = 0;
    }

    private void EnableOutline(GameObject pickup)
    {
        pickup.layer = 8;
    }

    private bool InPickupRange(GameObject pickup)
    {
        var diff = transform.position - pickup.transform.position;
        var distSq = diff.sqrMagnitude;
        // if it's within range to pick up we activate the pickup UI and disable player movement
        return distSq < pickupRange * pickupRange;
        
    }

    private void DoPickup(GameObject pickup)
    {
        if (currentPickup != null)
        {
            currentPickup.layer = 0;
        }

        pickup.layer = 8;
        repairCanvas.SetActive(true);
        Destroy(pickup);
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
