using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour, IBeatListener
{
    [SerializeField] private float _speed = 1f;

    private TriggerOnBeat _beatTrigger;

    protected void OnDisable()
    {
        _beatTrigger.Beat -= OnBeat;
    }

    public void Update()
    {
        if(_beatTrigger.CurrentTempo != 0)
        {
            float beatsPerSecond = _beatTrigger.CurrentTempo / 60;
            transform.position += transform.forward * beatsPerSecond * _speed * Time.deltaTime;
        }
    }

    protected void OnDestroy()
    {
        DOTween.KillAll();
    }

    public void SetBeatTrigger(TriggerOnBeat triggerOnBeat)
    {
        _beatTrigger = triggerOnBeat;

        triggerOnBeat.Beat += OnBeat;
    }

    private void OnBeat(object sender, System.EventArgs e)
    {
        Sequence scaleBounce = DOTween.Sequence();
        scaleBounce.Append(transform.DOScale(1.1f, 0.1f).SetEase(Ease.OutCubic));
        scaleBounce.Append(transform.DOScale(1f, 0.1f).SetEase(Ease.OutCubic));
        scaleBounce.Play();
    }
}
