using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class ItemCreatorEditor : EditorWindow {
    private enum ItemType {
        EQUIPMENT,
        WEAPON,
        ENEMY_DROP
    }

    private enum Zone {
        TUTORIAL,
        TEMP
    }

    private long itemId;
    private string itemName;
    private string itemDescription;
    private Sprite itemIcon;

    private EntityStats entityStats = new EntityStats(1);

    private ItemType itemType;
    private Zone zone;

    private int stackSize;
    private int levelRequirement;

    // Adds a menu item named "ItemCreator" to the RPG menu
    [MenuItem("RPG/ItemCreator")]
    public static void ShowWindow() {
        GetWindow(typeof(ItemCreatorEditor));
    }

    private void OnGUI() {
        GUILayout.Label("Item Base Settings", EditorStyles.boldLabel);
        itemId = EditorGUILayout.LongField("Item ID", itemId);
        itemName = EditorGUILayout.TextField("Item Name", itemName);
        itemDescription = EditorGUILayout.TextField("Item Description", itemDescription, GUILayout.Height(120));
        itemIcon = (Sprite)EditorGUILayout.ObjectField("Item Icon", itemIcon, typeof(Sprite), false);
        itemType = (ItemType)EditorGUILayout.EnumPopup("Item Type", itemType);

        GUILayout.Label("Item location settings", EditorStyles.boldLabel);
        zone = (Zone)EditorGUILayout.EnumPopup("Item Zone", zone);

        GUILayout.Label("Item Limits Settings", EditorStyles.boldLabel);
        stackSize = EditorGUILayout.IntField("Stack Size", stackSize);
        levelRequirement = EditorGUILayout.IntField("Level Requirement", levelRequirement);
        GUILayout.Label("Stat limits");
        entityStats.vitality = EditorGUILayout.IntField("Vitality Requirement", entityStats.vitality);
        entityStats.strength = EditorGUILayout.IntField("Strength Requirement", entityStats.strength);
        entityStats.intellect = EditorGUILayout.IntField("Intellect Requirement", entityStats.intellect);
        entityStats.dexterity = EditorGUILayout.IntField("Dexterity Requirement", entityStats.dexterity);

        if (GUILayout.Button("Create Item")) {
            Item item = CreateInstance<Item>();
            item.itemName = itemName;
            item.itemId = itemId;
            item.itemIcon = itemIcon;
            item.itemDescription = itemDescription;
            item.stackSize = stackSize;
            item.levelRequirement = levelRequirement;
            item.statRequirement = entityStats;
            string path = "Assets/Items/";
            path += itemType.ToString() + "/" + zone.ToString() + "/" + item.itemName + ".asset";
            AssetDatabase.CreateAsset(item, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log(JsonConvert.SerializeObject(item));
        }
    }

}
