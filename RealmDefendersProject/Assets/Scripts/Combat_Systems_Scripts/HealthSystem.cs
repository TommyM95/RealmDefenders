using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamageTaken;
    public event EventHandler OnDied;

    [SerializeField] private float maxHealth;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);   // Keep health going below zero and above max

        OnDamageTaken?.Invoke(this, EventArgs.Empty);

        if (IsDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsDead()
    {
        return currentHealth == 0;
    }

    public float GetCurrentHealthAmount()
    {
        return currentHealth;
    }

    public float GetCurrentHealthAmountNormalized()
    {
        return currentHealth / maxHealth;
    }

    public void SetMaxHealth(float maxHealth, bool updateCurrentHealth) 
    {
        this.maxHealth = maxHealth;

        if (updateCurrentHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public bool IsFullHealth()
    {
        if (currentHealth == maxHealth)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
