using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour,IDamageable {

    public int fullHealth=100;
    public bool isDestructive = false;

    //Effects 
    [HideInInspector]public GameObject particleEffect;

    [HideInInspector]public GameObject hitObj;

    [System.Serializable]
    public class InteractionEvent : UnityEvent { }
    public InteractionEvent OnDamage = new InteractionEvent();


    public void Damage(int amount)
    {
        fullHealth -= amount;
        if (fullHealth <= 0)
        {
            fullHealth = 0;
        }

        Destroy(hitObj, .1f);
        particleEffect.SetActive(true);
        OnDamage.Invoke();
       
    }

    public int GetFullHealth()
    {
        return fullHealth;
    }
}
