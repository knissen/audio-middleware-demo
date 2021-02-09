using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour, IBeatListener
{
    [SerializeField] private float _distancePerBeat = 1f;

    private TriggerOnBeat _beatTrigger;

    protected void OnDisable()
    {
        _beatTrigger.Beat -= OnBeat;
    }

    public void SetBeatTrigger(TriggerOnBeat triggerOnBeat)
    {
        _beatTrigger = triggerOnBeat;

        triggerOnBeat.Beat += OnBeat;
    }

    private void OnBeat(object sender, System.EventArgs e)
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.forward * _distancePerBeat;
    }
}
