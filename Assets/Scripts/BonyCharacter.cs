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

    public int GetHealth()
    {
        var health = 0;
        if (HasBone(BoneType.LeftLowerArm)) health++;
        if (HasBone(BoneType.LeftUpperArm)) health++;
        if (HasBone(BoneType.RightLowerArm)) health++;
        if (HasBone(BoneType.RightUpperArm)) health++;
        if (!headHasBeenHit) health++;
        return health;
    }

    public bool HasBone(BoneType bone) => bones.HasFlag(bone);
}
