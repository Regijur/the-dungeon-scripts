using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventary : MonoBehaviour
{
    public static Inventary inventary;
    public List<Weapon> weapons;
    public List<ConsumableItem> items;
    // Start is called before the first frame update
    void Awake()
    {
        if (inventary == null)
        {
            inventary = this;
        }
        else if(inventary != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void AddWeapon(Weapon weapon)
    {
        if(!weapons.Contains(weapon)) weapons.Add(weapon);
    }
    public void AddItems(ConsumableItem item)
    {
        items.Add(item);
    }

    public void RemoveItem(ConsumableItem item)
    {
        if (items.Contains(item)) items.Remove(item);
    }

    public void RemoveAllItens()
    {
        Weapon weapon = weapons[0];
        weapons.Clear();
        weapons.Add(weapon);
        items.Clear();
    }
}
