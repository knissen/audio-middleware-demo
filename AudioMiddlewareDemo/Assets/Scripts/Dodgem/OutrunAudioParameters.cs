using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutrunAudioParameters : MonoBehaviour
{
    public ScoreBoard scoreBoard;

    public int scoreToTriggerVocals = 10;
    [ParamRef] public string vocalParameter;
    public int scoreToTriggerSax = 30;
    [ParamRef] public string saxParameter;

    private bool _vocalHasBeenSet = false;
    
    protected void Update()
    {
        float vocalValue = scoreBoard.Score >= scoreToTriggerVocals ? 1f : 0f;

        if(vocalValue == 1 && !_vocalHasBeenSet)
        {
            _vocalHasBeenSet = true;
            RuntimeManager.StudioSystem.setParameterByName(vocalParameter, 1);
        }


        float saxValue = scoreBoard.Score >= scoreToTriggerSax ? 1f : 0f;
        RuntimeManager.StudioSystem.setParameterByName(saxParameter, saxValue);
    }
}
