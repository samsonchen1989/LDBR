using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DialogType
{
    DIALOG_HEAD_LEFT,
    DIALOG_HEAD_RIGHT,
    DIALOG_HINT
}

public class DialogManager : MonoBehaviour
{
    public GameObject dialogHeadLeft;
    public GameObject dialogHeadRight;
    public GameObject dialogHint;

    // Minimum show time between each dialog
    public float dialogInternal;
    float dialogTimer = 0;
    
    private static DialogManager instance;
    public static DialogManager Instance
    {
        get {
            if (instance == null) {
                Debug.LogError("Fail to get DialogManager instance");
            }

            return instance;
        }
    }

    void Awake()
    {
        if (instance != null) {
            Debug.LogError("Only one instance of DialogManager is allowwed");
        }

        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        if (dialogHeadLeft == null || dialogHeadRight == null || dialogHint == null) {
            Debug.LogError("Please assign dialog gameObject first.");
            return;
        }
    }

    public void PlayDialogData(List<DialogData> datas, Trigger trigger)
    {
        StartCoroutine(PlayDialogCoroutine(datas, trigger));
    }

    public IEnumerator PlayDialogCoroutine(List<DialogData> datas, Trigger trigger)
    {
        trigger.TiggerEnable = false;
        // Pause game
        Time.timeScale = 0;

        dialogTimer = 0;

        GameObject dialog = dialogHeadLeft;
        foreach (DialogData data in datas) {
            if (data.Type == DialogType.DIALOG_HEAD_LEFT) {
                dialog = dialogHeadLeft;
            } else if (data.Type == DialogType.DIALOG_HEAD_RIGHT) {
                dialog = dialogHeadRight;
            } else if (data.Type == DialogType.DIALOG_HINT) {
                dialog = dialogHint;
            }

            dialog.SetActive(true);
            if (dialog == dialogHint) {
                dialog.GetComponent<Dialog>().SetText(data.Text);
            } else {
                dialog.GetComponent<Dialog>().SetImage(data.ImageName);
                dialog.GetComponent<Dialog>().SetText(data.Text);
            }

            yield return StartCoroutine(WaitForKeyUp(KeyCode.Space));
            dialog.SetActive(false);
        }

        // Resume game
        Time.timeScale = 1;
        trigger.TiggerEnable = true;

        yield return null;
    }

    IEnumerator WaitForKeyUp(KeyCode keycode)
    {
        // Wait until key "Space" enter up and timer passed internal time
        while (!(Input.GetKeyUp(keycode) && (dialogTimer > dialogInternal))) {
            dialogTimer += Time.unscaledDeltaTime;
            yield return null;
        }
        
        dialogTimer = 0f;
    }
}
