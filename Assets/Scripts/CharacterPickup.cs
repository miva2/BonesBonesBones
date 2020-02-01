using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPickup : MonoBehaviour
{
    public GameObject repairCanvas;
    public double pickupRange;
    public BoneAttachHandler boneAttachHandler;

    BonyCharacter bonyCharacter;
    GameObject currentPickup;

    void Start()
    {
        bonyCharacter = GetComponent<BonyCharacter>();
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
        var pickedUpBone = pickup.GetComponent<Bone>();
        PickBoneTypes(pickup, pickedUpBone, out var rightBoneType, out var rightJointType);

        var leftBoneType = pickedUpBone.BoneType;
        var leftJointType = pickedUpBone.JointType;

        boneAttachHandler.CreateBones(leftBoneType, rightBoneType, leftJointType, rightJointType);

        Destroy(pickup);
    }

    private void PickBoneTypes(GameObject pickup, Bone pickedUpBone, out BoneType rightBoneType, out JointType rightJoinType)
    {
        // pickup should store how good the hit was so we can
        rightBoneType = BoneType.LeftLowerArm;
        rightJoinType = JointType.One;
    }

    BoneType[] allBoneTypes = new [] { BoneType.LeftLowerArm, BoneType.RightLowerArm, BoneType.LeftUpperArm, BoneType.RightUpperArm };

    private BoneType GetRandomBoneTypeExcept(BoneType rightBoneType)
    {
        var boneTypes = new List<BoneType>(allBoneTypes);
        boneTypes.Remove(rightBoneType);
        var index = UnityEngine.Random.Range(0, boneTypes.Count);
        return boneTypes[index];
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
