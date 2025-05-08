using UnityEngine;
using UnityEngine.UIElements;

public class CraftingManager : MonoBehaviour
{
    private Button craftButton1;
    private Button craftButton2;
    private Button craftButton3;
    public InventoryManager inventoryManager;
    public InventoryUIUpdater inventoryUIUpdater;

    private void OnEnable()
    {
        // Obtener la raíz del documento UI Toolkit
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Buscar los botones por nombre
        craftButton1 = root.Q<Button>("Craft1");
        craftButton2 = root.Q<Button>("Craft2");
        craftButton3 = root.Q<Button>("Craft3");

        // Asignar eventos de clic
        if (craftButton1 != null)
        {
            craftButton1.clicked += () =>
            {
                Debug.Log("Se pulsó el botón Craft1 (corresponde a Item_1)");
                if (inventoryManager.ConsumeItems(1, 1))
                {
                    inventoryUIUpdater.ChangeUI();
                }

            };
        }

        if (craftButton2 != null)
        {
            craftButton2.clicked += () =>
            {
                Debug.Log("Se pulsó el botón Craft1 (corresponde a Item_2)");
                if (inventoryManager.ConsumeItems(2, 3))
                {
                    inventoryUIUpdater.ChangeUI();
                }
            };
        }

        if (craftButton3 != null)
        {
            craftButton3.clicked += () =>
            {
                Debug.Log("Se pulsó el botón Craft1 (corresponde a Item_3)");
                if (inventoryManager.ConsumeItems(3, 4))
                {
                    inventoryUIUpdater.ChangeUI();
                }
            };
        }
    }
}
