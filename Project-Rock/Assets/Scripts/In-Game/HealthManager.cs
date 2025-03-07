﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public bool isPlayer1;
    public Slider healthSlider;
    public float maxHealth = 400;
    [SerializeField]
    private float currentHealth;
    private float largeHealthPoolMarker;
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        currentHealth = maxHealth;

        largeHealthPoolMarker = maxHealth * 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        if(GameManager.Instance.currentGameState == GameManager.GameState.Playing)
        {
            if (currentHealth <= largeHealthPoolMarker)
            {
                currentHealth -= damage / 4;
            }
            else
            {
                currentHealth -= damage;
            }

            if (currentHealth <= 0)
            {
                if (isPlayer1)
                {
                    GameManager.Instance.RoundWon(2);
                }
                else
                {
                    GameManager.Instance.RoundWon(1);
                }
            }
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
