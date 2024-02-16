using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject target;

    private OrderSystem orderSystem;
    private Order currentOrder;

    // Start is called before the first frame update
    void Start()
    {
        orderSystem = GameObject.Find("PizzaShop").GetComponent<OrderSystem>();
        orderSystem.SetCallback(NewOrderCallback);
    }

    void NewOrderCallback(Order newOrder)
    {
        // Function to give to OrderSystem Callback
        // Called when a new order is generated
        Debug.Log("Received new order: " + newOrder.customerName);
    }

    // Called when the pizza is delivered
    void DeliveryCallback(){
        Debug.Log("Delivered");
        orderSystem.DeliverOrder(currentOrder.orderNumber);
        currentOrder = null;
    }

    void SetOrder(int orderNumber)
    {
        // Find the order
        currentOrder = orderSystem.GetOrderInfo(orderNumber);

        GameObject targetObject = GameObject.Instantiate(target);
        targetObject.GetComponent<DeliveryTarget>().StartDelivery(currentOrder,DeliveryCallback);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentOrder == null){
            currentOrder = orderSystem.GetNextOrder();

            // Set the start target
            if(currentOrder != null){
                GameObject targetObject = GameObject.Instantiate(target);
                targetObject.GetComponent<DeliveryTarget>().StartDelivery(currentOrder,DeliveryCallback);
            }
        }
    }
}
