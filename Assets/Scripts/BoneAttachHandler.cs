using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoneAttachHandler : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject repairCanvas;
    public GameObject otherBone;

    RectTransform rectTransform;
    RectTransform otherRectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        otherRectTransform = otherBone?.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (otherRectTransform != null && BonesOverlap())
        {
            AttachBones();
        }
    }

    private bool BonesOverlap()
    {
        var first = RectTransformToScreenSpace(rectTransform);
        var second = RectTransformToScreenSpace(otherRectTransform);
        return first.Overlaps(second);
    }

    private void AttachBones()
    {
        Debug.Log("Bones overlap.");
        repairCanvas.SetActive(false);
    }

    static Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        return new Rect((Vector2)transform.position - (size * 0.5f), size);
    }
}
