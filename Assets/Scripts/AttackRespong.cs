using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRespong : MonoBehaviour
{
    [SerializeField]
    private bool attackEnable = false;
    private void OnTriggerEnter(Collider other) {
    }
        
    // private void OnMouseEnter() {
    //     if(attackEnable)
    //     else{
    //         Cursor.SetCursor(DefaultCursor, Vector2.zero, CursorMode.Auto);
    //     }

    private void AttackStarts()
    {
        attackEnable = true;
        print("Attack enabled");
    }
    private void AttackFinish()
    {
        attackEnable = false;
        print("Attack dissabled");
    }
    public void ChangeCursorToBattle(Texture2D BattleCursor){
        if(attackEnable)
            Cursor.SetCursor(BattleCursor, Vector2.zero, CursorMode.Auto);
            print("Battle cursor setted.");
    }
    public void ChangeCursorToRun(Texture2D DefaultCursor){
        if (attackEnable == false)
            Cursor.SetCursor(DefaultCursor, Vector2.zero, CursorMode.Auto);
            print("Default cursor setted;");
    }
}
