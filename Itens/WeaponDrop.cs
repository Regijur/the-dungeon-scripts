using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    public Weapon weapon;
    public InformationManager informationManager;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        informationManager = FindObjectOfType<InformationManager>(true);
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = weapon.image;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Mechanics player = collision.GetComponent<Mechanics>();
        if(player != null)
        {
            if (!Inventary.inventary.weapons.Contains(weapon))
            {
                informationManager.TakeInformation(weapon.messageOn);
                Inventary.inventary.AddWeapon(weapon);
                player.AddWeapon(weapon);
            }
            else
            {
                informationManager.TakeInformation(weapon.messageOFF);

            }
            Destroy(gameObject);
        }
    }
}
