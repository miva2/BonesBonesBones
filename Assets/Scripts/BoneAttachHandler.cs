using System;
using UnityEngine;
using UnityEngine.UI;

public class BoneAttachHandler : MonoBehaviour
{
    public BonyCharacter player;
    public Transform leftBoneStartingPos;
    public Transform rightBoneStartingPos;
    public GameObject bonePrefab;

    public Texture jointOneSprite;
    public Texture jointTwoSprite;
    public Texture jointThreeSprite;

    GameObject rightBoneObj;
    GameObject leftBoneObj;

    RectTransform leftRectTransform;
    RectTransform rightRectTransform;
    Bone leftBone;
    Bone rightBone;

    void Start()
    {
    }

    public void CreateBones(JointType leftJointType, JointType rightJointType, BoneType pickupBoneType)
    {
        // Create the left and right bone from the prefab and activate the bone UI
        leftBoneObj = Instantiate<GameObject>(bonePrefab, leftBoneStartingPos.position, Quaternion.identity, transform);
        leftRectTransform = leftBoneObj.GetComponent<RectTransform>();
        leftBone = leftBoneObj.GetComponent<Bone>();
        leftBone.JointType = leftJointType;
        leftBone.BoneType = pickupBoneType;
        var sprite = GetSprite(leftJointType);
        leftBone.GetComponent<RawImage>().texture = sprite;

        rightBoneObj = Instantiate<GameObject>(bonePrefab, rightBoneStartingPos.position, Quaternion.identity, transform);
        rightRectTransform = rightBoneObj.GetComponent<RectTransform>();
        rightBone = rightBoneObj.GetComponent<Bone>();
        rightBone.JointType = rightJointType;
        rightBone.BoneType = pickupBoneType;
        sprite = GetSprite(rightJointType);
        rightBone.GetComponent<RawImage>().texture = sprite;

        gameObject.SetActive(true);
    }

    private Texture GetSprite(JointType jointType)
    {
        switch (jointType)
        {
            case JointType.One:
                return jointOneSprite;
            case JointType.Two:
                return jointTwoSprite;
            case JointType.Three:
                return jointThreeSprite;
        }

        throw new Exception();
    }

    void Update()
    {
        if (leftRectTransform != null && rightRectTransform != null && BonesOverlap())
        {
            if (leftBone.JointType == rightBone.JointType)
            {
                player.AddBone(leftBone.BoneType);
                HideAndReset();
            }
            else
            {
                // TODO something when bones don't match
                HideAndReset();
            }
        }
    }

    private bool BonesOverlap()
    {
        var left = RectTransformToScreenSpace(leftRectTransform);
        var right = RectTransformToScreenSpace(rightRectTransform);
        return left.Overlaps(right);
    }

    private void HideAndReset()
    {
        gameObject.SetActive(false);
        Destroy(leftBoneObj);
        Destroy(rightBoneObj);
    }

    static Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        return new Rect((Vector2)transform.position - (size * 0.5f), size);
    }
}
