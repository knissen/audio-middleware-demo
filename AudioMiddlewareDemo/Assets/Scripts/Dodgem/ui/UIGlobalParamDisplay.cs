using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UIGlobalParamDisplay : MonoBehaviour
{
    [ParamRef] public string paramName;

    private TextMeshProUGUI _textBox;

    // Start is called before the first frame update
    protected void Awake()
    {
        _textBox = GetComponent<TextMeshProUGUI>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(FMODUnity.RuntimeManager.StudioSystem.getParameterByName(paramName, out float value) == FMOD.RESULT.OK)
        {
            _textBox.text = $"{paramName}: {value}";
        }
    }
}
