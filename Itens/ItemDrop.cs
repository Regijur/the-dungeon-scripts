using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public ConsumableItem item;
    public InformationManager informationManager;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        informationManager = FindObjectOfType<InformationManager>(true);
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = item.image;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            informationManager.TakeInformation(item.message);
            Inventary.inventary.AddItems(item);
            Destroy(gameObject);
        }
    }
}
