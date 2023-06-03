using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    Joystick joystick;
    private void Awake()
    {
        joystick = FindObjectOfType<Joystick>();
    }

    public Vector3 JoystickInput()
    {
        Vector3 direction = new Vector2();
        if (Input.GetMouseButton(0))
        {
            direction.x = joystick.Horizontal;
            direction.z = joystick.Vertical;
            float heading = Mathf.Atan2(direction.x * 50f, direction.z * 50f);
            if(direction.z >= 0.1f || direction.z <= -0.1f || direction.x >= 0.1f || direction.x <= -0.1f)
            {
                transform.rotation = Quaternion.Euler(0f, (heading * Mathf.Rad2Deg), 0f);
            }
            else
                direction = Vector3.zero;
        }
        return direction;
    }
}
