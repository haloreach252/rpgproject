[System.Serializable]
public class EntityStats {

    public int vitality, strength, intellect, dexterity;

    public EntityStats(int baseStats) {
        vitality = baseStats;
        strength = baseStats;
        intellect = baseStats;
        dexterity = baseStats;
    }

    public EntityStats(int vitality, int strength, int intellect, int dexterity) {
        this.vitality = vitality;
        this.strength = strength;
        this.intellect = intellect;
        this.dexterity = dexterity;
    }
}