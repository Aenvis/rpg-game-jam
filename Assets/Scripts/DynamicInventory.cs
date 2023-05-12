using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicInventory : MonoBehaviour
{
    public int maxItems = 3;
    public List<ItemData> items = new List<ItemData>();
    public void AddItem(ItemData itemToAdd)
    {
        Debug.Log("test1");
        for (int i = 0; i < maxItems; i++)
        {
            Debug.Log("test2");
            if (items[i] == null)
            {
                Debug.Log("test3");
                items[i] = itemToAdd;
                Debug.Log("test4");
                //return true;
            }
            Debug.Log("test5");
        }
        if (items.Count < maxItems)
        {
            items.Add(itemToAdd);
            //return true;
        }
        Debug.Log("No space in the inventory");
        //return false;
    }
    public bool DeleteItem(ItemData itemToDelete)
    {
        int position = FindItem(itemToDelete);
        if (position != -1)
        {
            items.Remove(itemToDelete);
            return true;
        }
        Debug.Log("You don't have this item in inventory");
        return false;
    }
    public int FindItem(ItemData itemToFind)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == itemToFind)
            {   
                return i;
            }
        }
        return -1;
    }
}