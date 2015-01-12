using UnityEngine;
using System.Collections;

public class InteractiveItem : MonoBehaviour
{
    public int itemID;

    public int ItemID
    {
        get {
            return itemID;
        }

        set {
            itemID = value;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player") {
            Inventory.Instance.AddStack(ItemPrefabsDefinition.StackClone(itemID, 1));
            GameObject.Destroy(this.gameObject);
        }
    }
}
