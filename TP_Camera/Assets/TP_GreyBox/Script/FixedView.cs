using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FixedView : AView
{
    public float yaw = 0;
    public float pitch = 90;
    public float roll = 0;
    public float fov = 60;
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
