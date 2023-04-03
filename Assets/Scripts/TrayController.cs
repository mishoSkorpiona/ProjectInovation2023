using System;
using UnityEngine;
using DG.Tweening;

public class TrayController : MonoBehaviour
{
    public float maxTiltAngle = 20.0f; // The maximum tilt angle that the tray can have
    public float gyroTiltFactor = 0.5f; // The factor to apply the tray tilt to the gyro tilt
    private float currentTiltAngle; // The current tilt angle of the tray
    private Rigidbody rb;
    [SerializeField] private FakeGyroscope fakeGyro;
    private float timeSinceLastBalance;
    [SerializeField] private float balanceInterval = 0.3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        timeSinceLastBalance += Time.deltaTime;
        if (timeSinceLastBalance >= balanceInterval)
        {
            BalanceTray();
            timeSinceLastBalance = 0.0f;
        }
    }

    void FixedUpdate()
    {
        ClampTrayAngle();
    }

    void BalanceTray()
    {
        //Debug.Log("Balance");

        Quaternion targetRotation = medianQuaternion(transform.rotation, fakeGyro.GetGyroRotation(), gyroTiltFactor);

        transform.DORotateQuaternion(targetRotation, balanceInterval * 0.7f);        

        
        rb.angularVelocity = Vector3.zero;
    }

    Quaternion medianQuaternion(Quaternion angle1, Quaternion angle2, float ratio)
    {
        // Clamp ratio between 0 and 1
        ratio = Mathf.Clamp01(ratio);

        // Calculate the angle between the two quaternions
        float angle = Quaternion.Angle(angle1, angle2);

        // If the angle is close to zero, just return one of the quaternions
        if (Mathf.Approximately(angle, 0.0f))
        {
            return angle1;
        }

        // Calculate the interpolation factor
        float t = Mathf.Sin((1.0f - ratio) * angle) / Mathf.Sin(angle);

        // Interpolate between the two quaternions using Slerp
        return Quaternion.Slerp(angle1, angle2, t);
    }


    void ClampTrayAngle()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.x = WrapAngle(currentRotation.x);
        currentRotation.z = WrapAngle(currentRotation.z);
        currentRotation.x = ClampTiltAngle(currentRotation.x);
        currentRotation.z = ClampTiltAngle(currentRotation.z);
        transform.rotation = Quaternion.Euler(currentRotation);
        rb.angularVelocity = Vector3.zero;
    }

    float WrapAngle(float angle)
    {
        angle %= 360;

        if (angle > 180)
            return angle - 360;

        if (angle < -180)
            return angle + 360;

        return angle;
    }

    float ClampTiltAngle(float angle)
    {
        return Mathf.Clamp(angle, -maxTiltAngle, maxTiltAngle);
    }

    void ApplyTilt()
    {
        Quaternion targetRotation = Quaternion.Euler(currentTiltAngle, 0, 0);
        rb.MoveRotation(targetRotation);
        rb.angularVelocity = Vector3.zero;
    }
    

    void OnDrawGizmos()
    {
        if (fakeGyro == null) return;
        Quaternion currentGyroRotation = fakeGyro.GetGyroRotation();
        Vector3 gyroTiltX = currentGyroRotation * Vector3.right;
        Vector3 gyroTiltZ = currentGyroRotation * Vector3.forward;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + gyroTiltX);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + gyroTiltZ);
    }
}