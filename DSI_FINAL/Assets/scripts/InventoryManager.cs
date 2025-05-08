using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InventoryManager : MonoBehaviour
{
    private int[] inventory = new int[25];
    private string ruta;

    // Inicializa el inventario con ceros
    void Start()
    {
        ruta = Path.Combine(Application.persistentDataPath, "guardado.json");
        for (int i = 0; i < inventory.Length; i++) inventory[i] = 0;
        if (File.Exists(ruta))
        {
            string contenido = File.ReadAllText(ruta);

            // Si es un array serializado, conviértelo desde JSON
            // Ejemplo: { "items": [1,2,3,0,0,4] }
            int[] array = JsonHelper.FromJson<int>(contenido);

            // Para cada número en el array, llama a AddItem()
            foreach (int numero in array)
            {
                AddItem(numero);
            }
        }
        
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
                guardar();
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

        // First, find two different indices that match the required items
        int i = 0;
        while( i < inventory.Length && !(firstIndex != -1 && secondIndex != -1))
        {
            if (inventory[i] == item1 && firstIndex == -1)
            {
                firstIndex = i;
            }
            else if (inventory[i] == item2 && secondIndex == -1)
            {
                secondIndex = i;
                
            }
            i++;
        }

        // If both items were found in different positions
        if (firstIndex != -1 && secondIndex != -1)
        {
            inventory[firstIndex] = 0;
            inventory[secondIndex] = 0;
            guardar();
            CompactInventory();
            return true;
        }

        return false;
    }
    public void guardar()
    {
        string json = JsonHelper.ToJson(inventory, true);
        File.WriteAllText(ruta, json);
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
