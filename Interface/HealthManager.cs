using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
   public Text healthText;
   public PlayerStats player;
    private void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        SetLife();     
    }
    public void SetLife()
    {
        healthText.text = $"{player.health}/{player.maxHealth}";
    }
}
