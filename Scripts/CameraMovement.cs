using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camera moves in x and z directions

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    [SerializeField] float cameraSpeed = 5.0f;
    [SerializeField] Vector3 offset;

    private float threshold = 0.2f;

    // Update is called once per frame
    void Update()
    {
        // Get the player direction
        Vector3 targetDirection =  playerObject.transform.position - transform.position + offset;
        float distance = targetDirection.magnitude;

        if(distance > threshold)
        {
            transform.position = transform.position + targetDirection.normalized*cameraSpeed*Time.deltaTime;
        }
        
    }
}
