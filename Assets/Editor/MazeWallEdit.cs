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
        var sceneCamera = SceneView.lastActiveSceneView.camera;

        switch (keyCode)
        {
            case KeyCode.F:
                return t =>
                {
                    var angles = t.rotation.eulerAngles;
                    angles.x *= -1;
                    angles.z *= -1;
                    t.rotation = Quaternion.Euler(angles);
                };
            case KeyCode.E:
                return t =>
                {
                    var angles = t.rotation.eulerAngles;
                    angles.z -= 90;
                    t.rotation = Quaternion.Euler(angles);
                };
            case KeyCode.Q:
                return t =>
                {
                    var angles = t.rotation.eulerAngles;
                    angles.z += 90;
                    t.rotation = Quaternion.Euler(angles);
                };
            case KeyCode.W:
                return t =>
                {
                    var scale = t.localScale;
                    scale.x += 1;
                    t.localScale = scale;
                };
            case KeyCode.S:
                return t =>
                {
                    var scale = t.localScale;
                    scale.x -= 1;
                    t.localScale = scale;
                };
            case KeyCode.LeftArrow:
                return t => {
                    t.Translate(-gridSize, 0, 0);
                    sceneCamera.transform.Translate(-gridSize, 0, 0);
                };
            case KeyCode.RightArrow:
                return t => {
                    t.Translate(gridSize, 0, 0);
                    sceneCamera.transform.Translate(gridSize, 0, 0);
                };
            case KeyCode.UpArrow:
                return t => {
                    t.Translate(0, -gridSize, 0);
                    sceneCamera.transform.Translate(0, -gridSize, 0);
                };
            case KeyCode.DownArrow:
                return t => {
                    t.Translate(0, gridSize, 0);
                    sceneCamera.transform.Translate(0, gridSize, 0);
                };
            case KeyCode.D:
                return t => {
                    var newObj = Instantiate<GameObject>(t.gameObject);
                    Selection.objects = new GameObject[] { newObj };
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