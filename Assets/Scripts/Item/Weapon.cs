using System;
using System.Collections.Generic;

[Serializable]
public class Weapon : Item {
    public List<DamageType> damageTypes;

    public float baseDamage;

    public float killExpBonus;
    public float lootBonus;
}
