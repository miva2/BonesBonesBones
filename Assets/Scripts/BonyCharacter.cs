using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonyCharacter : MonoBehaviour
{
    public BoneType bones;
    bool headHasBeenHit;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Hit(BoneType type)
    {
        Debug.Log($"Hit bone of type {type}.");

        var health = GetHealth();
        if (health == 1 && type == BoneType.Head)
        {
            headHasBeenHit = true;
            Die();
        }

        if (type != BoneType.Head)
        {
            // mask out the bonetype that was hit
            bones &= ~type;
            ThrowOutBone(type);
        }
        else
        {
            Stun();
        }
    }

    private void ThrowOutBone(BoneType type)
    {
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
