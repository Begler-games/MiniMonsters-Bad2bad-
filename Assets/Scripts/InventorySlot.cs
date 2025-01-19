using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI quantityText; // Assign UI Text for item quantity
    public Item item;
   public int quantity;
    private void Start()
    {
        PlayerPrefs.GetInt("Quantity", quantity);
    }

    public void AddItem(Item newItem)
    {
        quantity++;
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        quantityText.text = quantity.ToString(); // Update quantity display
        quantityText.enabled = true;
        Debug.Log($"Item {item.itemName} has quantity: {item.quantity}");
        PlayerPrefs.SetInt("Quantity", quantity);
        Debug.Log("sany "+quantity);
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        quantityText.text = "";
        quantityText.enabled = false;
        quantity = 0;
        PlayerPrefs.SetInt("Quantity", quantity);
    }
}
