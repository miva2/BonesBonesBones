using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScaler : MonoBehaviour
{
    Material material;
    public int uIndex = 0;
    public int vIndex = 1;
    public float multiplier = 1;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        var s = transform.localScale;
        var u = s[uIndex];
        var v = s[vIndex];
        material.mainTextureScale = new Vector2(u, v) * multiplier;
    }
}
