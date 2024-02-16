using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum TargetType {
    StartTarget,
    EndTarget
}
public class DeliveryTarget : MonoBehaviour
{
    // The target represents either the start point or the end point
    private TargetType targetType;
    private Order currentOrder;

    private Action deliveryAction;

    public void StartDelivery(Order newOrder, Action callback)
    {
        currentOrder = newOrder;
        targetType = TargetType.StartTarget;
        gameObject.transform.position = currentOrder.startPosition;
        deliveryAction = callback;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            if(targetType == TargetType.StartTarget){
                // Move to the end point
                gameObject.transform.position = currentOrder.deliveryPosition;
                targetType = TargetType.EndTarget;
            } else if(targetType == TargetType.EndTarget){
                if(deliveryAction != null){
                    deliveryAction();
                }

                Destroy(gameObject);
            }
        }
    }
}
