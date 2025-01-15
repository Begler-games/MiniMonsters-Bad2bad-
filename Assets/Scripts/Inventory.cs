using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public string itemName;
    public int quantity;
    public Sprite icon;
}

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public string saveFileName = "inventory.json";
    public GameObject slotPrefab;  // Reference to the Slot Prefab
    public Transform slotParent;   // Parent transform to hold the slots

    void Start()
    {
        LoadInventory();
        UpdateUI();
    }

    public void AddItem(string name, int qty, Sprite icon)
    {
        Item existingItem = items.Find(item => item.itemName == name);
        if (existingItem != null)
        {
            existingItem.quantity += qty;
            UpdateSlot(existingItem);
        }
        else
        {
            Item newItem = new Item() { itemName = name, quantity = qty, icon = icon };
            items.Add(newItem);
            CreateSlot(newItem);
            UpdateSlot(newItem);
        }
        SaveInventory();
        UpdateUI();
    }

    public void RemoveItem(string name, int qty)
    {
        Item existingItem = items.Find(item => item.itemName == name);
        if (existingItem != null)
        {
            existingItem.quantity -= qty;
            if (existingItem.quantity <= 0)
            {
                items.Remove(existingItem);
                DestroySlot(name);
            }
            else
            {
                UpdateSlot(existingItem);
            }
            SaveInventory();
        }
    }

    public void SaveInventory()
    {
        string json = JsonUtility.ToJson(new InventoryData(items), true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, saveFileName), json);
    }

    public void LoadInventory()
    {
        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            InventoryData data = JsonUtility.FromJson<InventoryData>(json);
            items = data.items;
            foreach (var item in items)
            {
                CreateSlot(item);
            }
        }
    }

    void CreateSlot(Item item)
    {
        GameObject slot = Instantiate(slotPrefab, slotParent);
        InventorySlot slotScript = slot.GetComponent<InventorySlot>();
        slotScript.AddItem(item);
    }

    void UpdateSlot(Item item)
    {
        foreach (Transform slot in slotParent)
        {
            InventorySlot slotScript = slot.GetComponent<InventorySlot>();
            if (slotScript.item.itemName == item.itemName)
            {
                slotScript.AddItem(item);
                break;
            }
        }
    }

    void DestroySlot(string name)
    {
        foreach (Transform slot in slotParent)
        {
            InventorySlot slotScript = slot.GetComponent<InventorySlot>();
            if (slotScript.item.itemName == name)
            {
                Destroy(slot.gameObject);
                break;
            }
        }
    }

    [System.Serializable]
    private class InventoryData
    {
        public List<Item> items;
        public InventoryData(List<Item> itemList)
        {
            items = itemList;
        }
    }

    void UpdateUI()
    {
        foreach (Transform slot in slotParent)
        {
            InventorySlot slotScript = slot.GetComponent<InventorySlot>();
            if (slotScript.item != null)
            {
                slotScript.AddItem(slotScript.item);
            }
            else
            {
                slotScript.ClearSlot();
            }
        }
    }

}
