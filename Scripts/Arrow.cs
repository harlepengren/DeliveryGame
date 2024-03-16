using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private GameObject playerObject;

    void Start(){
        playerObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject targetObject;

        // Find the target object - if it exists
        
        targetObject = GameObject.Find("DeliveryTarget(Clone)");

        // Calculate the direction
        if(targetObject != null && playerObject != null){
            // Get the player and target positions
            Vector2 player2D = new Vector2(playerObject.transform.position.x,playerObject.transform.position.z);
            Vector2 target2D = new Vector2(targetObject.transform.position.x,targetObject.transform.position.z);
            
            // Calculate a vector between the player and target
            Vector2 arrowDirection = target2D - player2D;
            arrowDirection.Normalize();

            Vector2 upArrow = new Vector2(-1,0);

            // Calculate the angle between the arroDirection and the up arrow
            float arrowAngle = Mathf.Floor(Mathf.Acos(UnityEngine.Vector2.Dot(upArrow,arrowDirection))/Mathf.PI*180);
            
            // Dot product finds the smallest angle between two vectors, therefore, we need to know
            // whether the sign should be positive or negative.
            if(arrowDirection.y > 0){
                arrowAngle *= -1;
            }

            // Rotate the arrow using euler angles
            gameObject.transform.rotation = UnityEngine.Quaternion.Euler(0,0,arrowAngle);
        }
        
    }
}
