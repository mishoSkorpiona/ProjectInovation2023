using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    public List<string> orderList = new List<string>(); // The list of orders to be fulfilled
    public Text orderText; // The text object displaying the orders

    // Add an order to the list of orders
    public void AddOrder(string order)
    {
        orderList.Add(order);
        UpdateOrderText();
    }

    // Remove an order from the list of orders
    public void RemoveOrder(string order)
    {
        orderList.Remove(order);
        UpdateOrderText();
    }

    // Update the order text to display the current list of orders
    private void UpdateOrderText()
    {
        string text = "Orders:\n";
        foreach (string order in orderList)
        {
            text += "- " + order + "\n";
        }
        orderText.text = text;
    }
}