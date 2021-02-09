using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarMovement : MonoBehaviour
{
    [SerializeField] private float _moveTime = 0.1f;
    [SerializeField] private Ease _moveEase = Ease.InElastic;

    private Tween _activeTween;

    // Update is called once per frame
    protected void Update()
    {
        Vector3 nextDelta = Vector3.zero;

        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if(transform.position.x > 0)
            {
                nextDelta = Vector3.left;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (transform.position.x < 4)
            {
                nextDelta = Vector3.right;
            }
        }

        if(nextDelta != Vector3.zero)
        {
            if (_activeTween != null && _activeTween.active)
                DOTween.Kill(_activeTween);

            _activeTween = transform.DOMove(transform.position + nextDelta, _moveTime).SetEase(_moveEase);
        }
    }
}
