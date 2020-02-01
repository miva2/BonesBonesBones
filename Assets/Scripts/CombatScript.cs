﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CombatScript : MonoBehaviour
{
    public bool attackEnabled = false;
    private GameObject currentEnemy;
    private Canvas BattleUi;
    /// <summary>Shade out image for dissabling BattleUi.</summary>
    [SerializeField]
   // private GameObject shadeOut;
    private int activeTriggers = 0;
    [SerializeField]
    private Camera MainCamera;

    private void OnTriggerEnter(Collider other) {
            // Enemy init \\
        if (other.tag == "Enemy")
        {
            currentEnemy = other.gameObject;
            activeTriggers++;
            MainCamera.orthographicSize = 15;
        }
        if (other.tag == "Enemy" && activeTriggers >= 1)
        {

            ShowBattleUI();
        }
        if(other.tag == "Enemy" && activeTriggers == 2)
        {
            //shadeOut.SetActive(false);
            BattleInit();
        }    
    }

    private void OnTriggerStay(Collider other) 
    {

    }

    private void OnTriggerExit(Collider other) {
        attackEnabled = false;

        if (other.tag == "Enemy") activeTriggers--;
        if(other.tag == "Enemy" && activeTriggers == 1){
             attackEnabled = false;
             BattleUi.SendMessage("AttackFinish");
        }
        if(other.tag == "Enemy" && activeTriggers <= 0)
        {
            BattleUi.gameObject.SetActive(false);
            MainCamera.orthographicSize = 20;

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
        BattleUi.SendMessage("AttackStarts");
        attackEnabled = true;
    }
    

}
