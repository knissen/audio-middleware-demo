using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeMaterialColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    public void RandomizeColor()
    {
       Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        MaterialPropertyBlock block = new MaterialPropertyBlock();

        meshRenderer.GetPropertyBlock(block);

        block.SetColor("_Color", randomColor);

        meshRenderer.SetPropertyBlock(block);
    }
}
