using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseDrag : MonoBehaviour
{
    Vector3 dragOffset;
    bool dragging = false;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            DoDrag();
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

    void DoDrag()
    {
        transform.position = Input.mousePosition + dragOffset;
    }
}
