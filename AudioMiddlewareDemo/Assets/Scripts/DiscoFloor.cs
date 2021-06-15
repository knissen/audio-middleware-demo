using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoFloor : MonoBehaviour
{
    [SerializeField] private Color[] _possibleColors = default;

    private MaterialColorSet[] _colorBlocks;

    // Start is called before the first frame update
    protected void Awake()
    {
        _colorBlocks = GetComponentsInChildren<MaterialColorSet>();
    }

    public void SetBlockColors()
    {
        for (int i = 0; i < _colorBlocks.Length; i++)
        {
            Color nextColor = _possibleColors[Random.Range(0, _possibleColors.Length - 1)];

            _colorBlocks[i].SetColor(nextColor);
        }
    }

    public void SetAllSameColor()
    {
        Color nextColor = _possibleColors[Random.Range(0, _possibleColors.Length - 1)];

        for (int i = 0; i < _colorBlocks.Length; i++)
        {
            _colorBlocks[i].SetColor(nextColor);
        }
    }
}
