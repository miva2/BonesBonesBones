using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    public BonyCharacter player;
    public GameObject[] healthBones;

    void Update()
    {
        var health = player.GetHealth();
        for (var i = 0; i < healthBones.Length; i++)
        {
            healthBones[i].SetActive(i < health);
        }
    }
}
