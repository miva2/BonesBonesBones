using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    private GameObject currentEnemy;
    private Canvas BattleUi;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy")
        {
            currentEnemy = other.gameObject;
            BattleInit();
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.tag == "Enemy"){
            BattleUi.gameObject.SetActive(false);
        }
    }

    /// <summary>First phase of battle.</summary>
    private void BattleInit(){
        BattleUi = currentEnemy.GetComponentInChildren<Canvas>(true);
        BattleUi.gameObject.SetActive(true);
    }
}
