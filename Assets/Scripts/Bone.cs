using System;
using UnityEngine;

public enum JointType
{
    One,
    Two,
    Three
}

[Flags]
public enum BoneType
{
    Head = 0,
    LeftUpperArm = 1 << 0,
    LeftLowerArm = 1 << 1,
    RightUpperArm = 1 << 2,
    RightLowerArm = 1 << 3
}

public class Bone : MonoBehaviour
{
    public BoneType BoneType;
    public JointType JointType;
}