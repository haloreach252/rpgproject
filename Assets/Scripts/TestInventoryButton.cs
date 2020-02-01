using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventoryButton : MonoBehaviour {

    public Entity entity;
    public Item testItem;

    public void AddItem() {
        if (entity.inventory.AddItem(testItem)) {
            Debug.Log("Added item");
        } else {
            Debug.Log("Did not add item");
        }
    }

}
