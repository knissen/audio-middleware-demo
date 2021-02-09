using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    // Update is called once per frame
    protected void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if(transform.position.x > 0)
            {
                transform.position += Vector3.left;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (transform.position.x < 4)
            {
                transform.position += Vector3.right;
            }
        }
    }
}
