using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FixedView : AView
{
    public float yaw = 10;
    public float pitch = 10;
    public float roll = 10;
    public float fov = 10;
    public float speed = 1;
    public override CameraConfiguration GetConfiguration()
    {
        CameraConfiguration config = new CameraConfiguration();
        config.yaw = yaw;
        config.pitch = pitch;
        config.roll = roll;
        config.fieldOfView = fov;
        config.pivot = transform.position;  
        config.distance = 0;
        config.speed = speed;
        print(" FIxed view : yaw : " + yaw + " pitch : " + pitch + " roll : " + roll + " fov : " + fov);
        return config;
    }
}
