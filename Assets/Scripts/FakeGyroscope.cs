using UnityEngine;

public class FakeGyroscope : MonoBehaviour
{
    public class GyroData {
        public float alpha;
        public float beta;
        public float gamma;
    }
    public static FakeGyroscope Instance;



    
    public float maxTiltAngle = 20.0f; // The maximum tilt angle that the phone can generate

    private Vector3 fakeAcceleration; // The fake accelerometer input
    
    [SerializeField]private Quaternion fakeRotation; // The fake gyroscope rotation

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Get the current fake gyroscope rotation
    public Quaternion GetGyroRotation()
    {
        return fakeRotation;
    }

    // Update the fake accelerometer input based on the given acceleration
    public void SetAcceleration(Vector3 acceleration)
    {
        fakeAcceleration = acceleration;
    }

    public void UpdateGyro(GyroData gyroData)
    {
        // Create a new instance of GyroData class with the given data
        GyroData data = new GyroData();
        
        fakeRotation = Quaternion.Euler(gyroData.alpha,gyroData.beta, gyroData.gamma);
    }






    // Get the current fake accelerometer input
    public Vector3 GetAcceleration()
    {
        return fakeAcceleration;
    }
}