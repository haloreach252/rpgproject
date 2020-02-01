using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

	public Image icon;
	public Button removeButton;
	public Text amountText;

	private ItemStack itemStack;
	private Item item;

	public void AddItem(ItemStack newItem) {
		Debug.Log("Adding an item to inventory slot");
		itemStack = newItem;
		item = newItem.item;

		amountText.text = newItem.currentCount.ToString();

		icon.sprite = item.itemIcon;
		icon.enabled = true;
		removeButton.interactable = true;
	}

	public void ClearSlot() {
		Debug.Log("Clearing slot");
		itemStack = null;
		item = null;

		amountText.text = string.Empty;

		icon.sprite = null;
		icon.enabled = false;
		removeButton.interactable = false;
	}

	public void OnRemoveButton() {
		bool isEmpty = PlayerInventory.instance.RemoveItem(itemStack);
		Debug.Log("remove button clicked and: " + isEmpty);
		if (isEmpty) ClearSlot();
	}

	public void UseItem() {
		if (itemStack != null) {
			itemStack.UseItem();
		}
	}

}
