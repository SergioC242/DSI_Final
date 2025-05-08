using UnityEngine;
using UnityEngine.UIElements;

public class CraftingManager : MonoBehaviour
{
    private Button craftButton1;
    private Button craftButton2;
    public InventoryManager inventoryManager;
    public InventoryUIUpdater inventoryUIUpdater;

    private void OnEnable()
    {
        // Obtener la ra�z del documento UI Toolkit
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Buscar los botones por nombre
        craftButton1 = root.Q<Button>("Craft1");
        craftButton2 = root.Q<Button>("Craft2");

        // Asignar eventos de clic
        if (craftButton1 != null)
        {
            craftButton1.clicked += () =>
            {
                Debug.Log("entro");
                if (inventoryManager.ConsumeItems(1, 1))
                {
                    Debug.Log("Se puls� el bot�n Craft1 (corresponde a Item_1)");
                    inventoryUIUpdater.ChangeUI();
                }

            };
        }

        if (craftButton2 != null)
        {
            craftButton2.clicked += () =>
            {
                Debug.Log("Se puls� el bot�n Craft2 (corresponde a Item_2)");
                // Aqu� va la l�gica de crafteo para el Item_2
            };
        }
    }
}
