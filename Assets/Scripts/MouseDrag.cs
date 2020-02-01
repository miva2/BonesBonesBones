using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseDrag : MonoBehaviour
{
    Vector3 dragOffset;
    bool dragging = false;

    void Start()
    {
    }

    public void StartDrag()
    {
        Debug.Log("StartDrag");
        dragging = true;
        dragOffset = gameObject.transform.position - Input.mousePosition;
    }

    public void UpdateDrag()
    {
        Debug.Log("UpdateDrag");
        gameObject.transform.position = Input.mousePosition + dragOffset;
    }

    public void EndDrag()
    {
        Debug.Log("EndDrag");
        dragging = false;
    }

}
