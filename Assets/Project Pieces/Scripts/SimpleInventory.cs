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

    void UpdateInventoryUI()
    {
        inventoryText.text = "Inventory:\n";

        foreach (InventoryItem item in items)
        {
            inventoryText.text += item.itemName + " x" + item.count + "\n";
        }
    }
}