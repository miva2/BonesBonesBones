using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IndicatorLogic : MonoBehaviour
{
        /// <summary>BattleUI GameObject;</summary>
        private GameObject battleCanvas;
        [SerializeField]
        private float indicatorMoveSpeed = 0.3f;
        /// <summary>Indicator Position.</summary>
        private Vector2 inPo;
        [SerializeField]
        /// <summary>
        ///  attack points
        /// </summary>
        public GameObject[] attackPoints;


    private int attackPointIndex = 0;
    private Vector3 nextDestination;
    private float markerDistanceAllowance = 0.01f; 

        
    private void Start()
    {
        battleCanvas = this.GetComponentInParent<Canvas>().gameObject;
        nextDestination = attackPoints[attackPointIndex].transform.position;

        //Debug.Log("attack point 0: " + attackPoints[0]);
        //Debug.Log("attack point 1: " + attackPoints[1]);
        //Debug.Log("attack point 2: " + attackPoints[2]);
        //Debug.Log("next destination" + nextDestination);
        //Debug.Log("transform position: " + transform.position);
    }
    private void Update()
    {
        Indicator_AutoMove();
    }

    public void Attack()
    {
        print("HIT!");
    }

    private void Indicator_AutoMove(){
        if (battleCanvas.activeInHierarchy == true)
        {
            if (IsDestinationReached(nextDestination)) {
                nextDestination = GetNextDestination();
                Debug.Log("target reached, going to next one: " + nextDestination);
            }
            transform.position = Vector3.MoveTowards(transform.position, nextDestination, indicatorMoveSpeed * Time.deltaTime);
        }
    }

    Vector3 GetNextDestination()
    {
        int amount = attackPoints.Length;

        if (attackPointIndex < amount - 1)
        {
            attackPointIndex++;
        }
        else
        {
            attackPointIndex = 0;
        }

        return attackPoints[attackPointIndex].transform.position;
    }

    bool IsDestinationReached(Vector3 targetDestination)
    {
        var dist = Vector3.Distance(transform.position, targetDestination);

        if (Input.GetKeyUp(KeyCode.A))
        {
            Debug.Log("current: " + transform.position);
            Debug.Log("target: " + targetDestination);
            Debug.Log("reached?: " + (dist <= markerDistanceAllowance));
        }

        return dist <= markerDistanceAllowance;
    }
}
