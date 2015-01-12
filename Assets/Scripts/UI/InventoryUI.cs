using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryUI : MonoBehaviour
{
    public GameObject itemSlot;

    // Use this for initialization
    void Start()
    {
        if (itemSlot == null) {
            Debug.LogError("Define item slot game object first.");
        }

        InitInventoryList();
    }

    void InitInventoryList()
    {
        for (int i = 0; i < Inventory.Instance.InventorySize; i++) {
            GameObject go = GameObject.Instantiate(itemSlot) as GameObject;
            if (go != null) {
                go.transform.SetParent(gameObject.transform);
                go.transform.localScale = Vector3.one;

                InventorySlotUI slot = go.GetComponent<InventorySlotUI>();
                if (slot != null) {
                    slot.InitSlot(i);
                }
            }
        }
    }
}
