using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRespong : MonoBehaviour
{
    [SerializeField]
    private Texture2D SwordCursor;
    private bool atttackStarts = false; 
    private void OnMouseEnter() {
        Cursor.visible = false;
        // Cursor.SetCursor(SwordCursor, Vector2.zero, CursorMode.Auto);
    }
}
