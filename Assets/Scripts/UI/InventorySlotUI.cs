using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventorySlotUI : MonoBehaviour
{
    public RawImage image;

    int slotID;
    ItemStack stack;

    public void InitSlot(int id)
    {
        slotID = id;
    }

    void OnEnable()
    {
        Messenger.AddListener(MyEventType.INVENTORY_CHANGED, RefreshSlot);
    }

    void OnDisable()
    {
        Messenger.RemoveListener(MyEventType.INVENTORY_CHANGED, RefreshSlot);
    }

    // Use this for initialization
    void Start()
    {
        if (image == null) {
            Debug.LogError("Assign inventory slot's raw image gameobject first.");
            return;
        }

        Inventory.Instance.InventoryChanged += RefreshSlot;

        RefreshSlot();
    }

    void RefreshSlot()
    {
        stack = Inventory.Instance.inventory[slotID];
        if (stack != null && stack.num > 0) {
            image.gameObject.SetActive(true);
            image.texture = Resources.Load("Icon/" + stack.item.itemName) as Texture;
        } else {
            image.gameObject.SetActive(false);
        }
    }
}
