using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [HideInInspector]public int currentHealth;
    Damageable _damageableScript;

    private void Start()
    {
        _damageableScript = GetComponent<Damageable>();
        RetriveCurrentHealth();
    }

    public void PlayerDamage()
    {
        RetriveCurrentHealth();

        if (currentHealth <= 0)
        {
            Death();
        }
       // Debug.Log("Player Damage called in Player");
    }

    private void Death()
    {
        Debug.Log("Player Died");
    }

    void RetriveCurrentHealth()
    {
        if (_damageableScript != null)
        {
            currentHealth = _damageableScript.GetFullHealth();
            Debug.Log("Player Current Health is : " + currentHealth);
        }
    }
}
