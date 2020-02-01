using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScaler : MonoBehaviour
{
    Material material;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        var s = transform.localScale;
        material.mainTextureScale = new Vector2(s.x, s.y);
    }
}
