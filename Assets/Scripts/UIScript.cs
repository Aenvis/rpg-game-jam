using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIScript : MonoBehaviour
{
    public DynamicInventory inventory;
    public GameObject[] slots;
    public ItemData[] items;
    
    public void GetInventory()
    {
        items = inventory.items;
        for(int i = 0; i<items.Length; i++)
        {
            if (items[i] == null)
            {
                slots[i].SetActive(false);
            }
            else
            {
                slots[i].SetActive(true);
            }
        }
    }
    void Start()
    {
        InventoryEvent.Instance.MyEvent.AddListener(GetInventory);
    }
    
}
