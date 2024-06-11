using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        // Save the initial position and rotation of the camera
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // Reset the camera's position and rotation to the initial values
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}
