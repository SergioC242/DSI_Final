using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUIUpdater : MonoBehaviour
{
    //public VisualTreeAsset bagTemplate;
    public InventoryManager inventoryManager;
    private VisualElement root;

    private void Start()
    {
        inventoryManager.AddItem(1);
        inventoryManager.AddItem(2);
        inventoryManager.AddItem(4);
        inventoryManager.AddItem(1);
        inventoryManager.PrintInventory();
        ChangeUI();
    }
    private void OnEnable()
    {
        ChangeUI();
    }
    public void ChangeUI()
    {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        VisualElement bagContainer = root.Q<VisualElement>("BagContainer");

        int[] inventory = inventoryManager.GetInventory();

        //bagContainer.Clear();
        for (int i = 0; i < 25; i++)
        {
            var slotRoot = bagContainer.ElementAt(i);
            int itemID = inventoryManager.GetItemAt(i); //

            //int itemID = inventory[i];
            if (itemID > 0)
            {
                var tex = Resources.Load<Texture2D>($"item_{itemID}");
                //var tex = Resources.Load<Texture2D>($"item_1");
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
        }
    }
}
