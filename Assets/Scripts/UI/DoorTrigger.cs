using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorTrigger : Trigger
{
    public string hint;
    public string hintNext;

    public int keyItemID;
    public Transform portalTrans;

    List<DialogData> data;

    // Use this for initialization
    void Start()
    {
        if (hint == null) {
            Debug.LogError("Please assign door's default string first");
        }

        data = DialogDatabase.Instance.GetDialog(hint);
        needSpaceButton = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (data == null) {
            return;
        }

        if (!triggerEnable) {
            return;
        }

        string dialogName = "";

        if (needSpaceButton) {
            if (Input.GetKey(KeyCode.Space)) {
                if (Inventory.Instance.InventoryContains(keyItemID)) {
                    dialogName = hintNext;
                    data = DialogDatabase.Instance.GetDialog(dialogName);
                    if (data == null) {
                        return;
                    }

                    this.enabled = false;
                    this.gameObject.AddComponent<Portal>().targetTrans = portalTrans;
                    Inventory.Instance.Remove(keyItemID, 0);
                }

                DialogManager.Instance.PlayDialogData(dialogName, data, this);
            }
        } else {
            DialogManager.Instance.PlayDialogData(dialogName, data, this);
        }
    }
}
