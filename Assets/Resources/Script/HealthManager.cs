using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    public SpriteRenderer[] healthIcons;

    private void Start()
    {
        currentHealth = maxHealth;
        healthIcons = GameObject.Find("HealthUI").GetComponentsInChildren<SpriteRenderer>();
        UpdateHealthUI();
    }


    public void TakeDamage(int amount)
    {
        if (amount > 0)
        {
            currentHealth -= amount;
            UpdateHealthUI();

            if (currentHealth <= 0)
            {
                GameOver();
            }
        }
    }

    public void ExitDamage(int amount)
    {
        if (amount > 0)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            UpdateHealthUI();
        }
    }


    private void UpdateHealthUI()
    {
        int sortingOrder = 0; 

        for (int i = 0; i < healthIcons.Length; i++)
        {
            if (i < currentHealth)
            {
                healthIcons[i].gameObject.SetActive(true);
                healthIcons[i].sortingOrder = sortingOrder; 
                sortingOrder++; 
            }
            else
            {
                healthIcons[i].gameObject.SetActive(false);
            }
        }
    }



    private void GameOver()
    {
        Debug.Log("Game Over");
    }
}
