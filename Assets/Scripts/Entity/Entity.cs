using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

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

    // Basic values
    [SerializeField]
    protected float health;
    [SerializeField]
    protected float maxHealth = 100f;
    [SerializeField]
    protected int armor = 1;

    // Stats
    protected int strength = 10;
    protected int vitality = 10;
    protected int intellect = 10;
    protected int dexterity = 10;

    // Bonuses/resistances
    [SerializeField]
    protected float healingBonus = 0;
    [SerializeField]
    protected List<DamageType> resistances;
    [SerializeField]
    protected List<DamageType> vulnerabilities;

    // Level related stuff
    protected int level;

    protected virtual void Start() {
        // TODO: Scriptable object entity definitions
        SetInitialValues();
    }

    protected void SetInitialValues() {
        health = maxHealth;
    }

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

    public virtual void Heal(float heal) {
        float finalHeal = heal + healingBonus;
        finalHeal = Mathf.Clamp(finalHeal, 1, maxHealth - health);
        health += finalHeal;
    }

    protected virtual void Die() {
        //TODO: Actual death stuff --- depends on entity type
        Destroy(gameObject);
    }

}
