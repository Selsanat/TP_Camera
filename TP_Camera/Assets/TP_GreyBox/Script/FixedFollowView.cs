using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFollowView : AView
{
    public float _roll;
    public float _fov = 60;
    public Transform _target;
    public Transform _centralPoint;
    public float _yawOffsetMax = 45f;
    public float _pitchOffsetMax = 20f;
    private float _yawCentral;
    private float _pitchCentral;
    public override CameraConfiguration GetConfiguration()
    {
        CameraConfiguration config = new CameraConfiguration();

        if(_target == null || _centralPoint == null)
        {
            Debug.LogWarning("Target or central point is null");
            return config;
        }
        Vector3 direction = (_target.position - _centralPoint.position).normalized;

        float yaw = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float pitch = -Mathf.Asin(direction.y) * Mathf.Rad2Deg;

        Vector3 centralDirection = (_centralPoint.position - _target.position).normalized;
        float yawCentral = Mathf.Atan2(centralDirection.x, centralDirection.z) * Mathf.Rad2Deg;
        float pitchCentral = -Mathf.Asin(centralDirection.y) * Mathf.Rad2Deg;

        yaw = ApplyYawConstraints(yaw);
        pitch = ApplyPitchConstraints(pitch);

        config.yaw = yaw;
        config.pitch = pitch;
        config.roll = _roll;
        config.fieldOfView = _fov;
        config.pivot = this.transform.position;
        config.distance = 0;

        return config;
    }

    private float ApplyYawConstraints(float yaw)
    {
        float yawDiff = Mathf.DeltaAngle(_yawCentral, yaw);
        if(Mathf.Abs(yawDiff) > _yawOffsetMax)
        {
            yaw = _yawCentral + Mathf.Sign(yawDiff) * _yawOffsetMax;
        }
        return yaw;
    }
    private float ApplyPitchConstraints(float pitch)
    {
        float pitchDiff = Mathf.DeltaAngle(_pitchCentral, pitch);
        if(Mathf.Abs(pitchDiff) > _pitchOffsetMax)
        {
            pitch = _pitchCentral + Mathf.Sign(pitchDiff) * _pitchOffsetMax;
        }
        return pitch;
    }

}
