using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    protected float health;
    protected float maxHealth = 100f;
    protected int armor = 1;

    protected void Start() {
        health = maxHealth;
    }

    public void TakeDamage(float dam) {
        health -= dam;
        if(health <= 0) {
            Die();
        }
    }

    public void Heal(float heal) {
        health += heal;
        if(health > maxHealth) {
            health = maxHealth;
        }
    }

    protected void Die() {
        //TODO: Actual death stuff
        Destroy(gameObject);
    }

    /* GETTERS AND SETTERS */
    public float Health {
        get {
            return health;
        } set {
            health = value;
        }
    }

    public float MaxHealth {
        get {
            return maxHealth;
        } set {
            maxHealth = value;
        }
    }

    public int Armor {
        get {
            return armor;
        } set {
            armor = value;
        }
    }

}
