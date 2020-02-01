using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

	#region Instancing
	public static PlayerInventory instance;

	private void Awake() {
		if(instance != null) {
			Debug.Log("Inventory of player exists");
			return;
		}
		instance = this;
	}
	#endregion

	public GameObject inventoryUI;
	public GameObject slotPrefab;
	public Item[] testItems;

	private InventorySlot[] slots;

	private EntityInventory inventory;
	private bool isOpen;

	private void Start() {
		isOpen = false;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.E)) {
			ToggleUI();
		}

		if (Input.GetKeyDown(KeyCode.F)) {
			Add(testItems[Random.Range(0, testItems.Length)]);
		}
	}

	public void SetupUI() {
		inventory = GetComponent<PlayerEntity>().inventory;
		int slotAmount = inventory.GetInventorySize();
		int rows = Mathf.CeilToInt(slotAmount / 6.0f);
		if (rows < 1) rows = 1;
		Debug.Log(rows);

		inventoryUI.GetComponent<RectTransform>().sizeDelta = new Vector2(645, (rows * 90) + ((rows + 1) * 15));

		for (int i = 0; i < slotAmount; i++) {
			Instantiate(slotPrefab, inventoryUI.transform);
		}
		slots = inventoryUI.GetComponentsInChildren<InventorySlot>();
		UpdateUi();
	}

	public void ToggleUI() {
		isOpen = !isOpen;
		inventoryUI.SetActive(isOpen);
	}

	public bool Add(Item item) {
		bool a = inventory.AddItem(item);
		UpdateUi();
		return a;
	}

	public bool RemoveItem(ItemStack item) {
		inventory.RemoveItem(item);
		Debug.Log(item.currentCount);
		UpdateUi();
		if (item.currentCount == 0) {
			return true;
		}
		return false;
	}

	private void UpdateUi() {
		for (int i = 0; i < slots.Length; i++) {
			if(i < inventory.GetCurrentInventorySize()) {
				slots[i].AddItem(inventory.GetItem(i));
			} else {
				slots[i].ClearSlot();
			}
		}
	}

}
