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
        float nextX = -10;

        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if(transform.position.x > 0)
            {
                nextX = Mathf.RoundToInt(transform.position.x) - 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (transform.position.x < 4)
            {
                nextX = Mathf.RoundToInt(transform.position.x) + 1;
            }
        }

        if(nextX != -10)
        {
            if (_activeTween != null && _activeTween.active)
                _activeTween.Complete();

            _activeTween = transform.DOMoveX(nextX, _moveTime).SetEase(_moveEase);
        }
    }
}
