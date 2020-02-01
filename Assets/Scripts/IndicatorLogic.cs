using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IndicatorLogic : MonoBehaviour
{
        /// <summary>BattleUI GameObject;</summary>
        private GameObject father;
        [SerializeField]
        private float indicatorMoveSpeed = 0.3f;
        [SerializeField]
        private Vector3 moveVector = new Vector3(1,0,0);
        /// <summary>Indicator Position.</summary>
        private Vector2 inPo;
        [SerializeField]
        public GameObject[] aP;
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
            // inPo = this.transform.position;
            // switch(inPo)
            // {
            //     Vector2.MoveTowards(transform.position, aP[2].transform.position, indicatorMoveSpeed);
            //     case :
            //     Vector2.MoveTowards(transform.position, aP[0].transform.position, indicatorMoveSpeed);
            //     case :
            //     Vector2.MoveTowards(transform.position, aP[1].transform.position, indicatorMoveSpeed);
            // };



            transform.Translate(moveVector * indicatorMoveSpeed * Time.deltaTime);

        }
    }
}
