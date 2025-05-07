using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUIUpdater : MonoBehaviour
{
    public VisualTreeAsset bagTemplate;
    public InventoryManager inventoryManager;
    private VisualElement root;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        VisualElement bagContainer = root.Q<VisualElement>("BagContainer");

        int[] inventory = inventoryManager.GetInventory();

        bagContainer.Clear();
        for (int i = 0; i < inventory.Length; i++)
        {
            TemplateContainer slot = bagTemplate.CloneTree();
            VisualElement slotRoot = slot.Q<VisualElement>("Slot");

            int itemID = inventory[i];
            if (itemID > 0)
            {
                var tex = Resources.Load<Texture2D>($"Sprites/Items/item_{itemID}");
                if (tex != null)
                {
                    slotRoot.style.backgroundImage = new StyleBackground(tex);
                }
                else
                {
                    Debug.LogWarning($"No se encontró imagen para item_{itemID}");
                    slotRoot.style.backgroundImage = null;
                }
            }
            else
            {
                slotRoot.style.backgroundImage = null;
            }

            bagContainer.Add(slot);
        }
    }
}
