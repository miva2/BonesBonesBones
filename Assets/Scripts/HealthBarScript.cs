using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public BonyCharacter player;
    public GameObject rightLowerBone;
    public GameObject rightUpperBone;
    public GameObject leftLowerBone;
    public GameObject leftUpperBone;
    public float missingPickupAlpha = 0.25f;

    RawImage rightLowerImage;
    RawImage rightUpperImage;
    RawImage leftLowerImage;
    RawImage leftUpperImage;

    Outline rightLowerOutline;
    Outline rightUpperOutline;
    Outline leftLowerOutline;
    Outline leftUpperOutline;

    BoneType? currentPickupBone;
    RawImage currentPickupBoneImage;
    Outline currentPickupBoneOutline;

    void Start()
    {
        rightLowerImage = rightLowerBone.GetComponent<RawImage>();
        rightUpperImage = rightUpperBone.GetComponent<RawImage>();
        leftLowerImage = leftLowerBone.GetComponent<RawImage>();
        leftUpperImage = leftUpperBone.GetComponent<RawImage>();

        rightLowerOutline = rightLowerBone.GetComponent<Outline>();
        rightUpperOutline = rightUpperBone.GetComponent<Outline>();
        leftLowerOutline = leftLowerBone.GetComponent<Outline>();
        leftUpperOutline = leftUpperBone.GetComponent<Outline>();
    }

    void Update()
    {
        var cc = rightLowerImage.color;
        rightLowerImage.color = new Color(cc.r, cc.g, cc.b, GetAlpha(BoneType.RightLowerArm));
        rightUpperImage.color = new Color(cc.r, cc.g, cc.b, GetAlpha(BoneType.RightUpperArm));
        leftLowerImage.color = new Color(cc.r, cc.g, cc.b, GetAlpha(BoneType.LeftLowerArm));
        leftLowerImage.color = new Color(cc.r, cc.g, cc.b, GetAlpha(BoneType.LeftLowerArm));

        if (currentPickupBone != null)
        {
            var hasBone = player.HasBone(currentPickupBone.Value);
            var alpha = hasBone ? 1 : missingPickupAlpha;
            currentPickupBoneImage.color = new Color(cc.r, cc.g, cc.b, alpha);
        }
    }

    private float GetAlpha(BoneType type)
    {
        var hasBone = player.HasBone(type);
        if (hasBone) return 1;

        return type == currentPickupBone ? missingPickupAlpha : 0;
    }

    internal void SetPickupBone(BoneType pickupType)
    {
        UnsetPickupBone();

        currentPickupBone = pickupType;

        switch (pickupType)
        {
            case BoneType.RightLowerArm:
                currentPickupBoneImage = rightLowerImage;
                currentPickupBoneOutline = rightLowerOutline;
                break;
            case BoneType.RightUpperArm:
                currentPickupBoneImage = rightUpperImage;
                currentPickupBoneOutline = rightUpperOutline;
                break;
            case BoneType.LeftLowerArm:
                currentPickupBoneImage = leftLowerImage;
                currentPickupBoneOutline = leftLowerOutline;
                break;
            case BoneType.LeftUpperArm:
                currentPickupBoneImage = leftUpperImage;
                currentPickupBoneOutline = leftUpperOutline;
                break;
            default:
                throw new Exception();
        }

        currentPickupBoneOutline.enabled = true;
        var hasBone = player.HasBone(pickupType);
        var cc = currentPickupBoneImage.color;
        var alpha = hasBone ? 1 : missingPickupAlpha;
        currentPickupBoneImage.color = new Color(cc.r, cc.g, cc.b, alpha);
    }

    internal void UnsetPickupBone()
    {
        if (currentPickupBone == null) return;

        var hasBone = player.HasBone(currentPickupBone.Value);
        var cc = currentPickupBoneImage.color;
        var alpha = hasBone ? 1 : missingPickupAlpha;
        currentPickupBoneImage.color = new Color(cc.r, cc.g, cc.b, alpha);

        currentPickupBone = null;
        currentPickupBoneOutline.enabled = false;
    }
}
