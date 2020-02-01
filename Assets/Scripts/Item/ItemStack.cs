[System.Serializable]
public class ItemStack {
    public Item item;
    public int currentCount;

    public ItemStack() {
        currentCount = 0;
        item = null;
    }

    public ItemStack(Item item) {
        currentCount = 1;
        this.item = item;
    }

    public bool PutItem() {
        if(currentCount == item.stackSize || currentCount + 1 > item.stackSize) {
            return false;
        } else {
            currentCount++;
            return true;
        }
    }

    public Item PullItem() {
        currentCount--;
        return item;
    }

    public void UseItem() {
        item.Use();
        currentCount--;
    }

}
