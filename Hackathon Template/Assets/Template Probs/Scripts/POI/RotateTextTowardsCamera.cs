using UnityEngine;
using System.Collections;

public class RotateTextTowardsCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Get a reference to the main camera
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Calculate the direction from the text object to the main camera
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;

        // Rotate the text object to face the camera
        transform.rotation = Quaternion.LookRotation(directionToCamera);
    }
}