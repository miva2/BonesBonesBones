using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class RV_playerMovement : MonoBehaviour
{
    [SerializeField]
    private AudioSource walkingSound;

    [SerializeField]
    private float playerMovementSpeed = 0.3f;
    public GameObject playerMovePoint;
    private Transform pmo; // Player move object;
    private bool pmoSpawned, moving; 
    public static GameObject triggeringPMO;
    [SerializeField]
    private Collider battleRadius, walkingRadius;

    // Update is called once per frame
    void Update()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDistance = 0.0f;

        var pointerOnUI = EventSystem.current.IsPointerOverGameObject();
        if (!pointerOnUI && playerPlane.Raycast(ray, out hitDistance)){
            Vector3 mousePosition = ray.GetPoint(hitDistance);
            if(Input.GetMouseButtonDown(1)) // Right key pressed;
            {
                moving = true;
                if(pmoSpawned)
                {
                    pmo = null;
                    pmo = Instantiate(playerMovePoint.transform, mousePosition, Quaternion.identity);
                } else {
                    pmo = Instantiate(playerMovePoint.transform, mousePosition, Quaternion.identity);
                }
            }
        }
        if(pmo != null) {
            pmoSpawned =true;
        } else {
            pmoSpawned = false;
        }
        if(moving) {
            Move();
        }
    }
    private void Move()
    {
        if (pmo != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, pmo.transform.position, playerMovementSpeed);
            this.transform.LookAt(pmo.transform);

            if (!walkingSound.isPlaying)
            {
                walkingSound.Play();
            }
        }
        else
        {
            if (walkingSound.isPlaying)
            {
                walkingSound.Stop();
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {

        if(other.tag == "PMO" && walkingRadius){
            triggeringPMO = other.gameObject;
            triggeringPMO.GetComponent<PMO>().DestroyPMO();
        }    
    }
}
