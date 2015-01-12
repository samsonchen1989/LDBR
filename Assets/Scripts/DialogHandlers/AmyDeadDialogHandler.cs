using UnityEngine;
using System.Collections;

public class AmyDeadDialogHandler : MonoBehaviour
{
    public GameObject zombieSpawner;
    int lootItemID = 0;

    string listenDialog = "dialog2";

    void OnEnable()
    {
        Messenger.AddListener(listenDialog, AmyDeadHandler);
    }

    void OnDisable()
    {
        Messenger.RemoveListener(listenDialog, AmyDeadHandler);
    }

    void Start()
    {
        if (zombieSpawner == null) {
            Debug.LogError("Please assign zombie spawner first.");
            return;
        }
    }

    void AmyDeadHandler()
    {
        // Add loot
        GameObject itemObject = ItemPrefabsDefinition.ItemClone(lootItemID).itemObject;
        if (itemObject == null) {
            return;
        }

        GameObject.Instantiate(itemObject, this.gameObject.transform.position, Quaternion.identity);

        PlayerBase.Instance.PlayerEquip.ReplaceSecondWeapon(new Rifle());
        zombieSpawner.GetComponent<Spawner>().StartSpawner();
    }
}
