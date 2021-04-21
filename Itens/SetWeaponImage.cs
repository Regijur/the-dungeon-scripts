using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetWeaponImage : MonoBehaviour
{
    public Weapon weapon;

    public Image profile;
    public Text weaponName;
    public Text description;
    void Start()
    {
        SetData();
    }

    void SetData()
    {
        profile.sprite = weapon.image;
        weaponName.text = weapon.weaponName;
        description.text = weapon.description;
    }
}
