using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool pauseMenu = false;
    public GameObject optionPanel;
    public GameObject itemList;
    public GameObject ItemListPrefab;
    public RectTransform content;
    public ToggleGroup groupContent;

    [Header("Game Info")]
    public Text healthText;
    public Text staminaText;
    public Text damageText;
    public Text strengthText;
    public Text weaponEquippedText;

    [Header("Set Enemy Health")]
    public Text enemyHealth;
    public Enemy[] enemies;
    private Transform group;

    private PlayerStats player;
    private Mechanics mechanics;
    private Inventary inventary;
    public List<ItemList> items;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioMnager>(true).SetAudio();
        mechanics = GameObject.FindGameObjectWithTag("Player").GetComponent<Mechanics>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        inventary = Inventary.inventary;
        SetEnemyHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Q))
        {
            pauseMenu = !pauseMenu;
            itemList.SetActive(false);
            optionPanel.SetActive(true);
            if (pauseMenu)
            {
                Time.timeScale = 0.0f;
                pausePanel.SetActive(true);
                UpdateAtributtes();
            }
            else
            {
                Time.timeScale = 1.0f;
                pausePanel.SetActive(false);
                if(items.Count > 0)RefreshItemList();
                items.Clear();
            }
        }
    }

    public void ShowOption(int option)
    {
        optionPanel.SetActive(false);
        itemList.SetActive(true);
        UpdateList(option);
    }

    
    public void UpdateList(int option)
    {
        
        if(option == 0)
        {
            for (int i = 0; i < inventary.weapons.Count; i++)
            {
                GameObject tempItem = Instantiate(ItemListPrefab, content.transform); ;
                tempItem.GetComponent<ItemList>().SetUpWeapon(inventary.weapons[i]);
                if (tempItem.GetComponent<ItemList>().weapon == mechanics.weaponEquipped) tempItem.GetComponentInChildren<Toggle>().isOn = true;
                else tempItem.GetComponentInChildren<Toggle>().isOn = false;
                items.Add(tempItem.GetComponent<ItemList>());
                
            }
        }
        if(option == 1)
        {
            for (int i = 0; i < inventary.items.Count; i++)
            {
                GameObject tempItem = Instantiate(ItemListPrefab, content.transform); ;
                tempItem.GetComponent<ItemList>().SetUpItem(inventary.items[i]);
                items.Add(tempItem.GetComponent<ItemList>());
            }
        }
    }

    void RefreshItemList()
    {
        ItemList[] curentItems = content.GetComponentsInChildren<ItemList>();
        foreach(ItemList item in curentItems)
        {
            Destroy(item.gameObject);
        }
    }

    void UpdateAtributtes()
    {
        healthText.text = $"Vida: {player.health}/{player.maxHealth}";
        strengthText.text = $"Força de Ataque Base: {player.strength}";
        weaponEquippedText.text = $"Arma Equipada: {mechanics.weaponEquipped.weaponName}";
        staminaText.text = $"Fôlego: {player.stamina}";
        damageText.text = $"Dano Total: {player.strength + mechanics.weaponEquipped.damage}";
        FindObjectOfType<HealthManager>().SetLife();
    }

    public void SetWeapon()
    {
        if(groupContent.GetFirstActiveToggle().GetComponentInParent<ItemList>().weapon != null)
        {
            mechanics.weaponEquipped = (groupContent.GetFirstActiveToggle().GetComponentInParent<ItemList>().weapon);
            mechanics.GetComponentInChildren<Atack>().SetWeapon(groupContent.GetFirstActiveToggle().GetComponentInParent<ItemList>().weapon.damage);
            GameObject.Find("WeaponSprite").GetComponent<Image>().sprite = mechanics.weaponEquipped.image;
            mechanics.SetWeapon();
        }
        if (player.health < player.maxHealth)
        {
            if (groupContent.GetFirstActiveToggle().GetComponentInParent<ItemList>().consumableItem != null)
            {
                player.health += groupContent.GetFirstActiveToggle().GetComponentInParent<ItemList>().consumableItem.healthGain;
                if (player.health > player.maxHealth) player.health = player.maxHealth;

                {
                    inventary.RemoveItem(groupContent.GetFirstActiveToggle().GetComponentInParent<ItemList>().consumableItem);
                    Destroy(groupContent.GetFirstActiveToggle().GetComponentInParent<ItemList>().gameObject);
                }
            }
        }

        UpdateAtributtes();
    }

    public void SetEnemyHealth()
    {
        Text[] G = GameObject.FindGameObjectWithTag("EnemyHealthHUD").GetComponentsInChildren<Text>();
        if (G != null)
        {
            foreach (Text g in G)
            {
                Destroy(g.gameObject);
            }
        }
        group = GameObject.FindGameObjectWithTag("EnemyHealthHUD").transform;
        enemies = GameObject.FindObjectsOfType<Enemy>();
        foreach(Enemy e in enemies)
        {
            enemyHealth.GetComponent<Follow>().enemy = e.gameObject.transform;
            Instantiate(enemyHealth, group);
        }
    }
}
