using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightBox : MonoBehaviour
{

    public List<GameObject> menuItems;

    public void Highligth(int i)
    {
        ResetColor();
        var img = menuItems[i].GetComponent<Image>();
        img.color = Color.gray;
    }

    public void ResetColor()
    {
        foreach(var go in menuItems)
        {
            var img = go.GetComponent<Image>();
            img.color = Color.white;
        }
    }
}
