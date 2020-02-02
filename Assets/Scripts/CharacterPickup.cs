using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPickup : MonoBehaviour
{
    public GameObject repairUI;
    public double pickupRange;
    public BoneAttachHandler boneAttachHandler;
    public HealthBarScript healthScript;

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
                healthScript.UnsetPickupBone();
        }

        // update the closest pickup
        if (TryFindClosestPickup(out var pickup) && pickup != currentPickup)
        {
            if (currentPickup != null)
            {
                DisableOutline(currentPickup);
                currentPickup = null;
                healthScript.UnsetPickupBone();
            }

            // if it's within range we change current pickup and highlight it
            if (InPickupRange(pickup))
            {
                EnableOutline(pickup);
                currentPickup = pickup;
                var pickupType = currentPickup.GetComponent<Bone>().BoneType;
                healthScript.SetPickupBone(pickupType);
            }
        }

        // if we're not picking up a bone (UI is inactive) and we press the E key, do pickup
        if (!repairUI.activeSelf && Input.GetKeyDown(KeyCode.E))
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
        // dropped bones don't yet have a specific joint type so we can manipulate the odds
        // they are of a specific bone type though
        var pickedUpBone = pickup.GetComponent<Bone>();
        var pickupBoneType = pickedUpBone.BoneType;

        PickBoneTypes(pickup, pickedUpBone, out var leftJointType, out var rightJointType);

        boneAttachHandler.CreateBones(leftJointType, rightJointType, pickupBoneType);

        Destroy(pickup);
    }

    private void PickBoneTypes(GameObject pickupObj, Bone pickedUpBone, out JointType existingJointType, out JointType pickupJointType)
    {
        // Let's think about the rules:
        // - if the bonetype of the picked up bone is not a missing piece, we pick
        //   a random bonetype from the char and get a non-matching joint
        // - if the bone type is missing for the character:
        //   - if it is a lower part and the player has no matching upper part
        //     generate non-matching joint
        //   - if it is a lower part and the player has a matching upper part
        //     generate a random joint type that might match
        //   - if it is an upper part, always match joints

        var pickup = pickedUpBone.BoneType;
        var hasBone = bonyCharacter.HasBone(pickup);

        var forceMatchingJoints = (pickup == BoneType.LeftUpperArm && !hasBone) || (pickup == BoneType.RightUpperArm && !hasBone);

        var forceMismatchingJoints = hasBone || (pickup == BoneType.LeftLowerArm && !bonyCharacter.HasBone(BoneType.LeftUpperArm))
                                             ||  (pickup == BoneType.RightLowerArm && !bonyCharacter.HasBone(BoneType.RightUpperArm));

        existingJointType = (JointType) UnityEngine.Random.Range(0, 3);
        if (forceMatchingJoints) pickupJointType = existingJointType;
        else if (forceMismatchingJoints) pickupJointType = GetRandomJointTypeExcept(existingJointType);
        else pickupJointType = (JointType) UnityEngine.Random.Range(0, 3);
    }

    BoneType[] allBoneTypes = new [] { BoneType.LeftLowerArm, BoneType.RightLowerArm, BoneType.LeftUpperArm, BoneType.RightUpperArm };

    private BoneType GetRandomBoneTypeExcept(BoneType rightBoneType)
    {
        var boneTypes = new List<BoneType>(allBoneTypes);
        boneTypes.Remove(rightBoneType);
        var index = UnityEngine.Random.Range(0, boneTypes.Count);
        return boneTypes[index];
    }

    JointType[] allJointTypes = new [] { JointType.One, JointType.Two, JointType.Three };

    private JointType GetRandomJointTypeExcept(JointType except)
    {
        var boneTypes = new List<JointType>(allJointTypes);
        boneTypes.Remove(except);
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
