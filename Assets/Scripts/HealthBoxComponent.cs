using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoxComponent : MonoBehaviour
{
    PlayerComponent playerComponentHealth;

    public int healthBonus = 20;
    
    void Awake()
    {
        playerComponentHealth = FindObjectOfType<PlayerComponent>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag("Player") ||
            playerComponentHealth.health >= playerComponentHealth.maxHealth) 
            return;
        
        playerComponentHealth.health = playerComponentHealth.health + healthBonus;
        playerComponentHealth.healthBar.SetHealth(playerComponentHealth.health, playerComponentHealth.maxHealth);

        FindObjectOfType<AudioManager>().Play("playerHeal");

        Destroy(gameObject);
    }
    
}
