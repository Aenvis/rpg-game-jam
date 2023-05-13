using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DynamicInventory : MonoBehaviour
{
    public const int maxItems = 3;
    public ItemData[] items = new ItemData[maxItems] { null, null, null};
    public bool AddItem(ItemData itemToAdd)
    {
        for (int i = 0; i < maxItems; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
                InventoryEvent.Instance.MyEvent.Invoke();
                Debug.Log("Item added at position " + i);
                return true;

            }
        }
        Debug.Log("No space in the inventory");
        return false;
    }
    public bool DeleteItem(ItemData itemToDelete)
    {
        int position = FindItem(itemToDelete);
        if (position != -1)
        {
            items[position] = null;
            InventoryEvent.Instance.MyEvent.Invoke();
            Debug.Log("Item successuffly deleted from position " + position);
            return true;
        }
        Debug.Log("You don't have this item in inventory");
        return false;
    }
    public bool DeleteItemByPosition(int position)
    {
        if (items[position] != null)
        {
            items[position] = null;
            InventoryEvent.Instance.MyEvent.Invoke();
            Debug.Log("Item successuffly deleted from position " + position);
            return true;
        }
        Debug.Log("You don't have this item in inventory");
        return false;
    }
    public int FindItem(ItemData itemToFind)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == itemToFind)
            {   
                return i;
            }
        }
        return -1;
    }
}