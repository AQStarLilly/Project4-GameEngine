using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimpleInventory : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public string itemName;
        public int count;
    }

    public List<InventoryItem> items = new List<InventoryItem>();
    public TMP_Text inventoryText;
    public int maxSlots = 5;

    public bool AddItem(string itemName)
    {
        // Check if item exists to stack
        foreach (InventoryItem item in items)
        {
            if (item.itemName == itemName)
            {
                item.count++;
                UpdateInventoryUI();
                return true;
            }
        }

        // Check if inventory has space
        if (items.Count >= maxSlots)
        {
            Debug.Log("Inventory Full!");
            return false;
        }

        // Add new item
        InventoryItem newItem = new InventoryItem();
        newItem.itemName = itemName;
        newItem.count = 1;
        items.Add(newItem);

        UpdateInventoryUI();
        return true;
    }

    public bool HasItem(string itemName)
    {
        foreach (InventoryItem item in items)
        {
            if (item.itemName == itemName && item.count > 0)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveItem(string itemName, int amountToRemove = 1)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == itemName)
            {
                items[i].count -= amountToRemove;

                if (items[i].count <= 0)
                    items.RemoveAt(i);

                UpdateInventoryUI();
                return;
            }
        }
    }

    public int GetItemCount(string itemName)
    {
        foreach (InventoryItem item in items)
        {
            if (item.itemName == itemName)
            {
                return item.count;
            }
        }
        return 0;
    }

    void UpdateInventoryUI()
    {
        inventoryText.text = "Inventory:\n";

        foreach (InventoryItem item in items)
        {
            inventoryText.text += item.itemName + " x" + item.count + "\n";
        }
    }
}