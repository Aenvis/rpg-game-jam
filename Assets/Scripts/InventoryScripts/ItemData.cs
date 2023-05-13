using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Item/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject model;
    [TextArea]
    public string description;
}
