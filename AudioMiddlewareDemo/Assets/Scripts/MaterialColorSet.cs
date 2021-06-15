using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColorSet : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    public void RandomizeColor()
    {
       Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        SetColor(randomColor);
    }

    public void SetColor(Color color)
    {
        MaterialPropertyBlock block = new MaterialPropertyBlock();

        meshRenderer.GetPropertyBlock(block);

        block.SetColor("_Color", color);

        meshRenderer.SetPropertyBlock(block);
    }
}
