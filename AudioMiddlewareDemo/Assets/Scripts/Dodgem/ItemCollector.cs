using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private float _pickupRadius = 0.5f;
    [SerializeField] private LayerMask _pickupLayers = default;

    private Collider[] _hitBuffer = new Collider[5];

    private void Update()
    {
        if(ObjectInRange(out GameObject collectable))
        {
            ScoreBoard.Instance.AddPoints(1);
            Destroy(collectable);
        }
    }

    private bool ObjectInRange(out GameObject collectable)
    {
        int hits = Physics.OverlapSphereNonAlloc(transform.position, _pickupRadius, _hitBuffer, _pickupLayers);

        if(hits > 0)
        {
            collectable = _hitBuffer[0].transform.parent.gameObject;
            return true;
        }

        collectable = null;
        return false;
    }
}
