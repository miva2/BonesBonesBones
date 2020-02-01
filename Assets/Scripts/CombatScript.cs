using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CombatScript : MonoBehaviour
{
    private GameObject currentEnemy;
    private Canvas BattleUi;
    /// <summary>Shade out image for dissabling BattleUi.</summary>
    [SerializeField]
   // private GameObject shadeOut;
    private int activeTriggers = 0;

    private void OnTriggerEnter(Collider other) {
            // Enemy init \\
        if (other.tag == "Enemy")
        {
            currentEnemy = other.gameObject;
            // canvas = currentEnemy.GetComponentInChildren<Canvas>().gameObject;
            // shadeOut = GameObject.FindGameObjectWithTag("ShadeOut");
             activeTriggers++;
        }
        if (other.tag == "Enemy" && activeTriggers >= 1)
        {

            ShowBattleUI();
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.tag == "Enemy" && activeTriggers >= 2)
        {
            //shadeOut.SetActive(false);
            BattleInit();
        }    
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Enemy") activeTriggers--;
        if(other.tag == "Enemy" && activeTriggers <= 0)
        {
            BattleUi.gameObject.SetActive(false);
        }
    }

    /// <summary>First phase of battle.</summary>
    private void ShowBattleUI(){
        BattleUi = currentEnemy.GetComponentInChildren<Canvas>(true);
        BattleUi.gameObject.SetActive(true);
        //shadeOut.GetComponent<GameObject>().SetActive(true);
    }
    private void BattleInit(){
        print("You can attack bro!");
    }
}
