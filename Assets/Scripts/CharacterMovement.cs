using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private Vector3 targetPosition;

    //public LayerMask groundLayerMask; -- from video --> use collider instead



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // first method: This doesnt work. ScreenToWorldPoint() is not a magic method

        //if (Input.GetMouseButtonDown(0))
        //{
        //    targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    Debug.Log("TargetPosition = " + targetPosition);
        //}

        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5);



        // second method

        //Ray mousePointerRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hitInfo;

        //if(Physics.Raycast (mousePointerRay, out hitInfo, 100, groundLayerMask))
        //{
        //targetPosition = hitInfo.transform.position;
        // targetPosition is the location where the ray hits the ground collider

        //}

        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5);
    }
}
