using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Dialog + Shop/Upgrade UI
public class UITrigger : Trigger
{
    public GameObject targetUI;
    public string dialogName;

    List<DialogData> data;

    void OnEnable()
    {
        Messenger.AddListener(dialogName, ShowUI);
    }

    void OnDisable()
    {
        Messenger.RemoveListener(dialogName, ShowUI);
    }

    // Use this for initialization
    void Start()
    {
        if (dialogName == null || targetUI == null) {
            Debug.LogError("Please assign dialog name or target UI to UITrigger.");
            return;
        }

        data = DialogDatabase.Instance.GetDialog(dialogName);
        broadcast = true;
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

    void ShowUI()
    {
        if (targetUI == null) {
            return;
        }

        targetUI.SetActive(true);
    }
}
