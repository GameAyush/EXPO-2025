using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int itemValue = 1000; // Value of the collectible

    private bool canCollect = false;
    private InventorySystem playerInventory;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canCollect = true;
            playerInventory = other.GetComponent<InventorySystem>(); // Get inventory script
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canCollect = false;
            playerInventory = null;
        }
    }

    void Update()
    {
        if (canCollect && Input.GetKeyDown(KeyCode.E))
        {
            if (playerInventory != null)
            {
                playerInventory.CollectItem(itemValue); // Add value to inventory
                Destroy(gameObject); // Remove the item from the scene
            }
        }
    }
}
