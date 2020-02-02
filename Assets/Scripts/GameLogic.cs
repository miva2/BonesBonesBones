using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public BonyCharacter player;
    void Start()
    {
        timeToDamage = boneDestroyInterval;
    }

    public float boneDestroyInterval;
    float timeToDamage;

    // Update is called once per frame
    void Update()
    {
        timeToDamage -= Time.deltaTime;
        if (timeToDamage < 0)
        {
            DestoryBone();
            timeToDamage = boneDestroyInterval;
        }
    }

    private void DestoryBone()
    {
        var targets = new string[] { "LeftPoint", "RightPoint", "HeadPoint"};
        var target = targets[UnityEngine.Random.Range(0, 3)];
        player.TakeDamage(target, 1, null);
    }
}
