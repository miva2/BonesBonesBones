using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    private GameObject currentEnemy;
    private Canvas BattleUi;
    private void OnTriggerEnter(Collider other) {
        currentEnemy = other.gameObject;
        if (other.tag == "Enemy")
        {
            BattleInit();
        }
    }

    /// <summary>First phase of battle.</summary>
    private void BattleInit(){
        BattleUi = currentEnemy.GetComponentInChildren<Canvas>(true);
        BattleUi.gameObject.SetActive(true);
    }
}
