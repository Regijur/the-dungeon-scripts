using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    public Image image;
    public Text text;
    public ConsumableItem consumableItem;
    public Weapon weapon;
    public bool isSelected = false;
    public Toggle toggle;
    public void Start()
    {
        toggle.group = FindObjectOfType<UIManager>().groupContent;
    }
    public void SetUpItem(ConsumableItem item)
    {
        consumableItem = item;
        image.sprite = consumableItem.image;
        text.text = consumableItem.itemName;
    }
    public void SetUpWeapon(Weapon item)
    {
        weapon = item;
        image.sprite = weapon.image;
        text.text = weapon.weaponName;
    }

    public void Selected()
    {
        isSelected = true;
    }
}
