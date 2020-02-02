using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    /// <summary>This module/class contains all Response-in-battle (enemy side) logic.</summary>
public class AttackRespong : MonoBehaviour
{
    [SerializeField]
    private bool attackEnable = false;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Image AttackIndicator;

    private void AttackStarts()
    {
        attackEnable = true;
        // print("Attack enabled");
    }
    private void AttackFinish()
    {
        attackEnable = false;
        // print("Attack dissabled");
    }
    public void ChangeCursorToBattle(Texture2D BattleCursor){
        if(attackEnable)
            Cursor.SetCursor(BattleCursor, Vector2.zero, CursorMode.Auto);
            // print("Battle cursor setted.");
    }
    public void ChangeCursorToRun(Texture2D DefaultCursor){
        Cursor.SetCursor(DefaultCursor, Vector2.zero, CursorMode.Auto);
        // print("Default cursor setted;");
    }
    public void Hitted(Image HittedPoint){
        player.GetComponent<BonyCharacter>().TakeDamage(HittedPoint.name, Vector3.zero);
        AttackIndicator.GetComponent<IndicatorLogic>().ChangeIndicatorImage("Hit!");
    }

}
