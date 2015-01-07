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

    public float dialogInternal;
    float dialogTimer = 0;

    // Use this for initialization
    void Start()
    {
        if (dialogHeadLeft == null || dialogHeadRight == null || dialogHint == null) {
            Debug.LogError("Please assign dialog gameObject first.");
            return;
        }

        // Test
        List<DialogData> data = new List<DialogData>();
        data.Add(new DialogData("head_player", "Are you OK ?", DialogType.DIALOG_HEAD_LEFT));
        data.Add(new DialogData("head_zombie", "Uh ...", DialogType.DIALOG_HEAD_RIGHT));
        data.Add(new DialogData("head_player", "Oh no, freeze and hands up !", DialogType.DIALOG_HEAD_LEFT));
        data.Add(new DialogData("head_zombie", "Uhhhhhhhhhh ...", DialogType.DIALOG_HEAD_RIGHT));
        data.Add(new DialogData("head_player", "Shit, I say FREEZE !", DialogType.DIALOG_HEAD_LEFT));

        StartCoroutine(PlayDialogData(data));
    }
    
    // Update is called once per frame
    void Update()
    {
    
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

    IEnumerator PlayDialogData(List<DialogData> datas)
    {
        Time.timeScale = 0;
        dialogTimer = dialogInternal;

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
            dialog.GetComponent<Dialog>().SetImage(data.ImageName);
            dialog.GetComponent<Dialog>().SetText(data.Text);

            yield return StartCoroutine(WaitForKeyUp(KeyCode.Space));
            dialog.SetActive(false);
        }

        Time.timeScale = 1;

        yield return null;
    }
}
