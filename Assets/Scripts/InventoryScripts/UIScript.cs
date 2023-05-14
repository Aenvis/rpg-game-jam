using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public DynamicInventory inventory;
    public GameObject[] slots;
    public ItemData[] items;
    public Image bar;
    
    public void GetInventory()
    {
        items = inventory.items;
        for(int i = 0; i<items.Length; i++)
        {
            if (i >= slots.Length)
            {
                break;
            }
            else
            {
                if (items[i] == null)
                {
                    slots[i].SetActive(false);
                }
                else
                {
                    slots[i].GetComponent<Image>().sprite = items[i].icon;
                    slots[i].SetActive(true);
                }
            }
        }
    }
    void Start()
    {
        InventoryEvent.Instance.MyEvent.AddListener(GetInventory);
        GameManager.Instance.valueChanged.AddListener(UpdatePerMileMeter);
    }

    public void UpdatePerMileMeter()
    {
       bar.fillAmount  = GameManager.Instance._perMileMeter.Value / GameManager.Instance.startPerMileValue;
    }
    
}
