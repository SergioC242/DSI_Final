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

    // Verifica si existe una combinación y la consume si es posible
    public bool UseCombination(Dictionary<int, int> requiredItems)
    {
        // Contar los ítems actuales
        Dictionary<int, int> itemCounts = new Dictionary<int, int>();
        foreach (int item in inventory)
        {
            if (item == 0) continue;
            if (!itemCounts.ContainsKey(item))
                itemCounts[item] = 0;
            itemCounts[item]++;
        }

        // Verifica si hay suficientes
        foreach (var pair in requiredItems)
        {
            if (!itemCounts.ContainsKey(pair.Key) || itemCounts[pair.Key] < pair.Value)
                return false;
        }

        // Elimina los ítems requeridos
        Dictionary<int, int> toRemove = new Dictionary<int, int>(requiredItems);
        for (int i = 0; i < inventory.Length; i++)
        {
            int current = inventory[i];
            if (toRemove.ContainsKey(current) && toRemove[current] > 0)
            {
                inventory[i] = 0;
                toRemove[current]--;
            }
        }

        // Compactar inventario: mover a la izquierda
        CompactInventory();

        return true;
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
