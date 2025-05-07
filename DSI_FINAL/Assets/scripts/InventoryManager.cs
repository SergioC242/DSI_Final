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

    // A�ade un �tem en el primer hueco libre
    public bool AddItem(int item)
    {
        if (item < 1 || item > 4)
        {
            Debug.LogWarning("Item inv�lido.");
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

    // Verifica si existe una combinaci�n y la consume si es posible
    public bool UseCombination(Dictionary<int, int> requiredItems)
    {
        // Contar los �tems actuales
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

        // Elimina los �tems requeridos
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

    // Mueve todos los �tems hacia la izquierda para eliminar huecos
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

    // Mostrar el inventario en consola (opcional para depuraci�n)
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
