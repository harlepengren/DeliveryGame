using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapMovement : MonoBehaviour
{
    private float cameraHeight;
    private GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        cameraHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = playerObject.transform.position;

        newPosition.y = cameraHeight;

        gameObject.transform.position = newPosition;
        
    }
}
