using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimer : MonoBehaviour
{
    [SerializeField] private float _timeToLive = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Kill), _timeToLive);
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
