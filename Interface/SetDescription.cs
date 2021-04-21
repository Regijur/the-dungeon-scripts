using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDescription : MonoBehaviour
{
    ItemList itemList;
    void Start()
    {
        itemList = GetComponentInParent<ItemList>();
        if (itemList.weapon != null)  GetComponent<Text>().text = itemList.weapon.description;
        else if (itemList.consumableItem != null) GetComponent<Text>().text = itemList.consumableItem.description;

    }

}
