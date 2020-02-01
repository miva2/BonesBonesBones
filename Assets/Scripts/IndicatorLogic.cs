using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorLogic : MonoBehaviour
{
        /// <summary>BattleUI GameObject;</summary>
        private GameObject father;
        private float indicatorMoveSpeed = 0.3f;
        [SerializeField]
        private Vector3 moveVector;

    private void Start()
    {
        father = this.GetComponentInParent<Canvas>().gameObject;
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
        if (father.activeInHierarchy == true)
        {
            while(true){
                transform.Translate(moveVector * indicatorMoveSpeed * Time.deltaTime);
            }
        }
    }
}
