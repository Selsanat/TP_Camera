using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedView : AView
{
    public float yaw = 10;
    public float pitch = 10;
    public float roll = 10;
    public float fov = 10;

    public override CameraConfiguration GetConfiguration()
    {
        CameraConfiguration config = new CameraConfiguration();
        config.yaw = yaw;
        config.pitch = pitch;
        config.roll = roll;
        config.fieldOfView = fov;
        config.pivot = transform.position;  
        config.distance = 0;  
        return config;
    }
}
