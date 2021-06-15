using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleEnabled : MonoBehaviour
{
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
