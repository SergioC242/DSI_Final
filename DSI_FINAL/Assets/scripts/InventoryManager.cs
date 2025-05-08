using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private int[] inventory = new int[25];

    // Inicializa el inventario con ceros
    void Start()
    {
        for (int i = 0; i < inventory.Length; i++)
            inventory[i] = 0;
    }

    // Añade un ítem en el primer hueco libre
    public bool AddItem(int item)
    {
        if (item < 1 || item > 4)
        {
            Debug.LogWarning("Item inválido.");
            return false;
        }

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == 0)
            {
                inventory[i] = item;
                return true;
            }
        }

        Debug.Log("Inventario lleno.");
        return false;
    }

    // Devuelve una copia del inventario actual
    public int[] GetInventory()
    {
        return (int[])inventory.Clone();
    }

    // Devuelve el ítem en una posición específica
    public int GetItemAt(int index)
    {
        if (index < 0 || index >= inventory.Length)
            return 0;
        return inventory[index];
    }

    // Verifica si existe una combinación y la consume si es posible
    public bool ConsumeItems(int item1, int item2)
    {
        int firstIndex = -1;
        int secondIndex = -1;
        Debug.Log("entro"); Debug.Log("entro"); Debug.Log("entro");

        // First, find two different indices that match the required items
        int i = 0;
        while( i < inventory.Length && (firstIndex == -1 || secondIndex == -1))
        {
            if (inventory[i] == item1 && firstIndex == -1)
            {
                firstIndex = i;
            }
            else if (inventory[i] == item2 && secondIndex == -1)
            {
                secondIndex = i;
                
            }
        }

        // If both items were found in different positions
        if (firstIndex != -1 && secondIndex != -1)
        {
            inventory[firstIndex] = 0;
            inventory[secondIndex] = 0;
            //CompactInventory();
            return true;
        }

        return false;
    }

    // Mueve todos los ítems hacia la izquierda para eliminar huecos
    private void CompactInventory()
    {
        int[] newInventory = new int[25];
        int index = 0;

        foreach (int item in inventory)
        {
            if (item != 0)
            {
                newInventory[index++] = item;
            }
        }

        inventory = newInventory;
    }

    // Mostrar el inventario en consola (opcional para depuración)
    public void PrintInventory()
    {
        string output = "Inventario: ";
        foreach (int item in inventory)
        {
            output += item + " ";
        }
        Debug.Log(output);
    }
}
