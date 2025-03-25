using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public int totalValue = 0; // Total collected value
    public Text inventoryText; // UI Text to display collected value

    void Start()
    {
        UpdateInventoryUI();
    }

    public void CollectItem(int itemValue)
    {
        totalValue += itemValue; // Add item's value to total
        UpdateInventoryUI();
    }

    void UpdateInventoryUI()
    {
        
        inventoryText.text = "Value: $" + totalValue.ToString(); // Update UI
    }
}
