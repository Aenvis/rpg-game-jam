using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public ItemData item1;
    public ItemData item2;
    public ItemData item3;
    public ItemData item4;
    public ItemData item5;
    public DynamicInventory dynamicInventory;
    // Start is called before the first frame update
    void Start()
    {
        //dynamicInventory.AddItem(item1);
        //dynamicInventory.AddItem(item1);
        //dynamicInventory.DeleteItem(item1);
        //dynamicInventory.FindItem(item1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            dynamicInventory.DeleteItemByPosition(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            dynamicInventory.DeleteItemByPosition(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            dynamicInventory.DeleteItemByPosition(2);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            dynamicInventory.AddItem(item1);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            dynamicInventory.AddItem(item2);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            dynamicInventory.AddItem(item3);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            dynamicInventory.AddItem(item4);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            dynamicInventory.AddItem(item5);
        }
    }
}
