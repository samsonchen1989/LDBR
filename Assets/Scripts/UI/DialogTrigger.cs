using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogTrigger : Trigger
{
    public string[] dialogNames;

    List<DialogData> data;

    // Use this for initialization
    void Start()
    {
        if (dialogNames == null) {
            Debug.LogError("Do you forget assign dialog trigger's dialog name?");
            return;
        }

        data = new List<DialogData>();
        data.Add(new DialogData(null, "Door locked", DialogType.DIALOG_HINT));
        data.Add(new DialogData(null, "A key is needed", DialogType.DIALOG_HINT));
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!triggerEnable) {
            return;
        }

        if (Input.GetKey(KeyCode.Space)) {
            DialogManager.Instance.PlayDialogData(data, this);
        }
    }
}
