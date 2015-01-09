using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogTrigger : Trigger
{
    public string dialogName;

    List<DialogData> data;

    // Use this for initialization
    void Start()
    {
        if (dialogName == null) {
            Debug.LogError("Do you forget assign dialog trigger's dialog name?");
            return;
        }

        data = DialogDatabase.Instance.GetDialog(dialogName);
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

        if (needSpaceButton) {
            if (Input.GetKey(KeyCode.Space)) {
                DialogManager.Instance.PlayDialogData(dialogName, data, this);
            }
        } else {
            DialogManager.Instance.PlayDialogData(dialogName, data, this);
        }
    }
}
