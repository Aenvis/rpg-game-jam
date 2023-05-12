using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public ItemData item1;
    public DynamicInventory dynamicInventory;
    // Start is called before the first frame update
    void Start()
    {
        dynamicInventory = GetComponent<DynamicInventory>();
        Debug.Log(item1.name);
        dynamicInventory.AddItem(item1);
        dynamicInventory.AddItem(item1);
        dynamicInventory.DeleteItem(item1);
        dynamicInventory.FindItem(item1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
