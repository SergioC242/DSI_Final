using UnityEngine;

public class GenerateMat : MonoBehaviour
{
    public InventoryManager inventoryManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GenerateRand()
    {
        int randomNumber = Random.Range(1, 5);
        inventoryManager.AddItem(randomNumber);
    }
}
