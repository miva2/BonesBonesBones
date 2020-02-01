using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackTarget
{
    Left,
    Right,
    Head
}

public class BonyCharacter : MonoBehaviour
{
    public GameObject DroppedBonePrefab;
    public BoneType bones;
    public float boneDropDistance;
    public float boneDropElevation;
    public float boneDropForce;
    bool headHasBeenHit;

    // Start is called before the first frame update
    void Start()
    {
        bones = BoneType.LeftLowerArm | BoneType.LeftUpperArm | BoneType.RightLowerArm | BoneType.RightUpperArm;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Hit(AttackTarget.Left);
        }
    }

    public void Hit(AttackTarget target)
    {
        Debug.Log($"Hit bone of type {target}.");

        var health = GetHealth();
        if (health == 1 && target == AttackTarget.Head)
        {
            headHasBeenHit = true;
            Die();
        }

        if (target == AttackTarget.Head)
        {
            Stun();
        }
        else
        {
            BoneType hitBone;
            if (target == AttackTarget.Left)
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

    private void Stun()
    {
        Debug.Log("Stunned the enemy.");
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
