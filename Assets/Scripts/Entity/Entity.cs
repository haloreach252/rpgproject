using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Entity : MonoBehaviour {

    // Basic values
    [Header("Health")]
    [SerializeField]
    protected float health;
    [SerializeField]
    protected float maxHealth = 100f;
    [SerializeField]
    protected int armor = 1;

    // Stats
    [Space(5)]
    [Header("Stats")]
    [SerializeField]
    protected EntityStats entityStats;

    // Bonuses/resistances
    [Space(5)]
    [Header("Damage stuff")]
    [SerializeField]
    protected float healingBonus = 0;
    [SerializeField]
    protected List<DamageType> resistances;
    [SerializeField]
    protected List<DamageType> vulnerabilities;

    // Level related stuff
    [Space(5)]
    [Header("Level stuff")]
    [SerializeField]
    protected int level;
    [SerializeField]
    protected int xpGiven;

    [Space(5)]
    [Header("Inventory")]
    public EntityInventory inventory;

    protected virtual void Start() {
        // TODO: Scriptable object entity definitions
        // Ex: SetInitialValues(entity);
        // *** protected virtual void SetInitialValues(EntityObject entity) {}
        SetInitialValues();
    }

    /// <summary>
    /// Sets the initial values needed by all entities. Takes in an entity object definition.
    /// The entity object definition takes health, and base stats.
    /// </summary>
    protected virtual void SetInitialValues() {
        entityStats = new EntityStats(10);
        maxHealth += entityStats.vitality;

        inventory = new EntityInventory(8);

        level = 1;
        health = maxHealth;
    }

    // Todo: Bonuses and such will be applied here.
    protected virtual void UpdateEntity() {

    }

    // Todo: Chance resistances to percent based rather than flat value. (WoW vs DnD). Same with armor.
    public virtual void TakeDamage(float dam, List<DamageType> damageTypes) {
        foreach(DamageType dType in damageTypes) {
            if (resistances.Contains(dType)) {
                dam *= 0.5f;
            } else if (vulnerabilities.Contains(dType)) {
                dam *= 2.0f;
            }
        }
        float finalDam = dam - armor;

        health -= finalDam;
        if(health <= 0) {
            Die();
        }
    }

    // Heals the player according to their healing bonus and the heal amount
    public virtual void Heal(float heal) {
        float finalHeal = heal + healingBonus;
        finalHeal = Mathf.Clamp(finalHeal, 1, maxHealth - health);
        health += finalHeal;
    }

    protected virtual void Die() { }

}

public enum DamageType {
    BLUDGEON,
    FIRE,
    COLD,
    NECROTIC,
    PIERCING,
    SLASHING,
    PSYCHIC,
    RADIANT,
    POISON
}