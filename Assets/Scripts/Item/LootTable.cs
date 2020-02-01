using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot Table", order = 2)]
public class LootTable : ScriptableObject {
    public List<ItemDrop> loot;
}
