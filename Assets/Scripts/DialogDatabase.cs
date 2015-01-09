using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogDatabase : MonoBehaviour
{
    Dictionary<string, List<DialogData>> dialogDatabase;

    private static DialogDatabase instance;
    public static DialogDatabase Instance
    {
        get {
            if (instance == null) {
                Debug.LogError("Fail to get dialogDatabase instance");
            }

            return instance;
        }
    }

    void Awake()
    {
        if (instance != null) {
            Debug.LogError("Only one instance of DialogDatabase is allowwed");
        }

        instance = this;

        dialogDatabase = new Dictionary<string, List<DialogData>>();
        IniateDialogDatabase();
    }

    // Todo, read dialog data from external file in xml/json format
    void IniateDialogDatabase()
    {
        // Test data
        List<DialogData> data = new List<DialogData>();
        data.Add(new DialogData("head_player", "Are you OK ?", DialogType.DIALOG_HEAD_LEFT));
        data.Add(new DialogData("head_zombie", "Uh ...", DialogType.DIALOG_HEAD_RIGHT));
        data.Add(new DialogData("head_player", "Oh no, freeze and hands up !", DialogType.DIALOG_HEAD_LEFT));
        data.Add(new DialogData("head_zombie", "Uhhhhhhhhhh ...", DialogType.DIALOG_HEAD_RIGHT));
        data.Add(new DialogData("head_player", "Shit, I say FREEZE !", DialogType.DIALOG_HEAD_LEFT));
        dialogDatabase.Add("dialog1", data);

        data = new List<DialogData>();
        data.Add(new DialogData("head_player", "What happened here ?", DialogType.DIALOG_HEAD_LEFT));
        data.Add(new DialogData("head_amy", "The Undead ...\nEverything is out of control ...", DialogType.DIALOG_HEAD_RIGHT));
        data.Add(new DialogData("head_player", "I will take you to hospital.", DialogType.DIALOG_HEAD_LEFT));
        data.Add(new DialogData("head_amy", "I can not make it ...", DialogType.DIALOG_HEAD_RIGHT));
        data.Add(new DialogData("head_amy", "Watch out, they are coming!\nHold this gun...", DialogType.DIALOG_HEAD_RIGHT));
        data.Add(new DialogData(null, "You get rifle.", DialogType.DIALOG_HINT));
        dialogDatabase.Add("dialog2", data);

        data = new List<DialogData>();
        data.Add(new DialogData(null, "Door locked", DialogType.DIALOG_HINT));
        data.Add(new DialogData(null, "A key is needed", DialogType.DIALOG_HINT));
        dialogDatabase.Add("hint1", data);
    }

    public List<DialogData> GetDialog(string dialogName)
    {
        if (dialogDatabase.ContainsKey(dialogName)) {
            return dialogDatabase[dialogName];
        }

        return null;
    }

    // Use this for initialization
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
    
    }
}
