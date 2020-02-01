using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RV_playerMovement : MonoBehaviour
{
    [SerializeField]
    private float playerMovementSpeed = 0.3f;
    public GameObject playerMovePoint;
    private Transform pmo; // Player move object;
    private bool pmoSpawned;
    private bool moving;
    private Transform currPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDistance = 0.0f;

        if(playerPlane.Raycast(ray, out hitDistance)){
            Vector3 mousePosition = ray.GetPoint(hitDistance);
            // Quaternion tagetRotation = Quaternion.LookRotation(targetPoint = transform.position);
            if(Input.GetMouseButtonDown(0)) // Left key pressed;
            {
                moving = true;
                currPos = playerMovePoint.transform;
                if(pmoSpawned)
                {
                    pmo = null;
                    pmo = Instantiate(playerMovePoint.transform, mousePosition, Quaternion.identity);
                } else {
                    pmo = Instantiate(playerMovePoint.transform, mousePosition, Quaternion.identity);
                }
            }
        }
        if(pmo)
        {
            pmoSpawned =true;
        } 
        else
        {
            pmoSpawned = false;
        }
        if(moving)
        {
            Move();
        }

         
    }
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, pmo.transform.position, playerMovementSpeed);
        this.transform.LookAt(pmo.transform);
    }
    
    
}
