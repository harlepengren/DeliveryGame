using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Order {
    public int orderNumber;
    public String customerName;
    public String address;
    public Vector3 startPosition;
    public Vector3 deliveryPosition;
    public Time orderTime;
    public int pizzaCount;
    public float orderTotal;
}

public class OrderSystem : MonoBehaviour
{
    [SerializeField] int orderIntensity; // Helps with order volume calulation
    [SerializeField] Vector3 startLocation;
    [SerializeField] Rect randomPositionBounds;
    private List<float> nextOrderTime;  // Keeps track of when to generate next orders
    private List<Order> currentOrders;
    private float orderUpdateTime;  // When to create new set of order times
    private const float orderWindow = 15.0f;
    private int orderNumberIndex;

    private Action<Order> newOrderAction;

    // Start is called before the first frame update
    void Start()
    {
        orderNumberIndex = 0;

        nextOrderTime = new List<float>();
        currentOrders = new List<Order>();


        orderUpdateTime = orderWindow;
        CreateOrder();
        GenerateOrders();
    }

    public void SetCallback(Action<Order> callback){
        newOrderAction = callback;
    }

    private void GenerateOrders()
    {
        int numNextOrders = UnityEngine.Random.Range(0,orderIntensity);

        if(numNextOrders > 0){
            for(int index=0; index<numNextOrders; ++index){
                nextOrderTime.Add(UnityEngine.Random.Range(0.0f,orderWindow));
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        float x,y,z;
        LayerMask roadMask = LayerMask.GetMask("Roads");
        Vector3 newPosition;
        int count = 0;

        roadMask = LayerMask.GetMask("Roads");

        do {
            count++;
            y = 0.12f;
            x = UnityEngine.Random.Range(randomPositionBounds.x,randomPositionBounds.x+randomPositionBounds.width);
            z = UnityEngine.Random.Range(randomPositionBounds.y,randomPositionBounds.y+randomPositionBounds.height);

            newPosition = new Vector3(x,y,z);

        } while(!Physics.Raycast(newPosition,Vector3.down,Mathf.Infinity,roadMask));

        Debug.Log("New Position: " + newPosition + " Count: " + count);

        return newPosition;
    }

    private void CreateOrder()
    {
        Order newOrder = new Order();
        newOrder.orderNumber = orderNumberIndex++;
        newOrder.customerName = "Test"+UnityEngine.Random.Range(0,1000);
        newOrder.address = "Somewhere";
        newOrder.pizzaCount = UnityEngine.Random.Range(1,10);
        newOrder.orderTotal = newOrder.pizzaCount * 14.99f;
        newOrder.startPosition = startLocation;
        newOrder.deliveryPosition = GetRandomPosition();
        currentOrders.Add(newOrder);

        if(newOrderAction != null){
            newOrderAction(newOrder);
        }
    }

    public Order GetNextOrder()
    {
        if(currentOrders.Count > 0) {
            return currentOrders[0];
        } else {
            return null;
        }
    }

    public Order GetOrderInfo(int orderNum){
        int index=0;

        while(index<currentOrders.Count)
        {
            if(currentOrders[index].orderNumber==orderNum){
                return currentOrders[index];
            }
        }

        // Order not found
        Debug.Log("Order not found");
        return null;
    }

    public List<Order> GetOrders(){
        return currentOrders;
    }

    public void DeliverOrder(int orderNum){
        // Find the order and remove it
        for(int index = 0; index < currentOrders.Count; index++){
            if(currentOrders[index].orderNumber == orderNum){
                currentOrders.RemoveAt(index);
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        orderUpdateTime -= Time.deltaTime;
        if(orderUpdateTime < 0){
            // Add some order times to the list
            orderUpdateTime = orderWindow;
            GenerateOrders();
        } else if(nextOrderTime.Count > 0) {
            for(int index = 0; index < nextOrderTime.Count; ++index)
            {
                nextOrderTime[index] -= Time.deltaTime;
                if(nextOrderTime[index] < 0){
                    // Generate order
                    CreateOrder();
                    Debug.Log("Created Order: " + currentOrders[currentOrders.Count - 1]);
                    // Remove nextOrderTime
                    nextOrderTime.RemoveAt(index);
                }
            }
        }
        
    }
}
