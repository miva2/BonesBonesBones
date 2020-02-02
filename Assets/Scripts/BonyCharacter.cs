using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AttackTarget
{
    LeftPoint,
    RightPoint,
    HeadPoint
}

public class BonyCharacter : MonoBehaviour
{
    public GameObject DroppedBonePrefab;
    public BoneType bones;
    public float boneDropDistance = 4;
    public float boneDropElevation = .3f;
    public float boneDropForce = 100;
    public float stunSeconds = 3;

    bool headHasBeenHit;
    float stunTimeLeft;
    NavMeshAgent navMeshAgent;
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        bones = BoneType.LeftLowerArm | BoneType.LeftUpperArm | BoneType.RightLowerArm | BoneType.RightUpperArm;
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Hit("LeftPoint", Vector3.zero);
        }

        UpdateStunTime();
    }

    private void UpdateStunTime()
    {
        if (stunTimeLeft > 0)
        {
            stunTimeLeft -= Time.deltaTime;
            if (stunTimeLeft < 0)
            {
                stunTimeLeft = 0;
                if (navMeshAgent != null)
                    navMeshAgent.isStopped = false;
            }
        }
    }

    public void Hit(String target, Vector3 stunForce)
    {
        Debug.Log($"Hit bone of type {target}.");

        var health = GetHealth();
        if (health == 1 && target == "HeadPoint")
        {
            headHasBeenHit = true;
            Die();
        }

        if (target == "HeadPoint")
        {
            Stun(stunForce);
        }
        else
        {
            BoneType hitBone;
            if (target =="LeftPoint")
            {
                if (HasBone(BoneType.LeftLowerArm))
                {
                    hitBone = BoneType.LeftLowerArm;
                }
                else
                {
                    hitBone = BoneType.LeftUpperArm;
                }
            }
            else
            {
                if (HasBone(BoneType.RightLowerArm))
                {
                    hitBone = BoneType.RightLowerArm;
                }
                else
                {
                    hitBone = BoneType.RightUpperArm;
                }
            }

            // mask out the bonetype that was hit and throw it out
            bones &= ~hitBone;
            ThrowOutBone(hitBone);
        }
    }

    private void ThrowOutBone(BoneType type)
    {
        var angle = UnityEngine.Random.Range(0, 2 * Mathf.PI);
        var dir = new Vector3(Mathf.Cos(angle), boneDropElevation, Mathf.Sin(angle)).normalized;
        var offsetVec = dir * boneDropDistance;

        var pickup = Instantiate<GameObject>(DroppedBonePrefab, transform.position + offsetVec, Quaternion.identity);

        var rb = pickup.GetComponent<Rigidbody>();

        var pickupBone = pickup.GetComponent<Bone>();
        pickupBone.BoneType = type;
        var jointType = (JointType) UnityEngine.Random.Range(0, 3);
        pickupBone.JointType = jointType;

        var forceVec = offsetVec * boneDropForce;
        rb.AddForce(forceVec);
    }

    private void Die()
    {
        Debug.Log("Character died.");
    }

    private void Stun(Vector3 force)
    {
        stunTimeLeft = stunSeconds;
        if (navMeshAgent != null)
            navMeshAgent.isStopped = true;
        
        if (rigidbody != null)
            rigidbody.AddForce(force);
    }

    public int GetHealth()
    {
        var health = 0;
        if (!headHasBeenHit) health++;
        if (HasBone(BoneType.LeftLowerArm)) health++;
        if (HasBone(BoneType.LeftUpperArm)) health++;
        if (HasBone(BoneType.RightLowerArm)) health++;
        if (HasBone(BoneType.RightUpperArm)) health++;
        return health;
    }

    public bool HasBone(BoneType bone) => bones.HasFlag(bone);
}
