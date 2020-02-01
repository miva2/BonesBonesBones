using UnityEngine;
using UnityEditor;
using System;

[InitializeOnLoad]
[CustomEditor(typeof(MazeWallEditor), true)]
[CanEditMultipleObjects]
public class MazeWallEdit : Editor
{
    public int gridSize = 5;

    private void OnSceneGUI()
    {
        var e = Event.current;

        if (e.type == EventType.KeyDown)
        {
            HandleKeyPress(e.keyCode);
        }
    }

    private void HandleKeyPress(KeyCode keyCode)
    {
        var transforms = Selection.transforms;
        Action<Transform> selectionAction = GetSelectedKeyAction(keyCode);
        Action otherAction = GetOtherKeyAction(keyCode);

        if (selectionAction == null && otherAction == null) return;

        // absorb the event
        Event.current.Use();

        if (selectionAction != null)
        {
            foreach (var t in transforms)
            {
                selectionAction(t);
            }
        }

        otherAction?.Invoke();
    }

    private Action<Transform> GetSelectedKeyAction(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.F:
                return t =>
                {
                    var angles = t.rotation.eulerAngles;
                    angles.x *= -1;
                    t.rotation = Quaternion.Euler(angles);
                };
            case KeyCode.LeftArrow:
                return t => t.Translate(-gridSize, 0, 0);
            case KeyCode.RightArrow:
                return t => t.Translate(gridSize, 0, 0);
            case KeyCode.UpArrow:
                return t => t.Translate(0, -gridSize, 0);
            case KeyCode.DownArrow:
                return t => t.Translate(0, gridSize, 0);
            case KeyCode.D:
                return t =>
                {
                    Instantiate<GameObject>(t.gameObject);
                };
        }

        return null;
    }

    private Action<Transform> GetAllSelectedKeyAction(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.F:
                return t =>
                {
                    var angles = t.rotation.eulerAngles;
                    angles.x *= -1;
                    t.rotation = Quaternion.Euler(angles);
                };
            case KeyCode.LeftArrow:
                return t => t.Translate(-gridSize, 0, 0);
            case KeyCode.RightArrow:
                return t => t.Translate(gridSize, 0, 0);
            case KeyCode.UpArrow:
                return t => t.Translate(0, -gridSize, 0);
            case KeyCode.DownArrow:
                return t => t.Translate(0, gridSize, 0);
            case KeyCode.D:
                return t =>
                {
                    Instantiate<GameObject>(t.gameObject);
                };
        }

        return null;
    }

    private Action GetOtherKeyAction(KeyCode keyCode)
    {
        switch (keyCode)
        {
        }

        return null;
    }

}