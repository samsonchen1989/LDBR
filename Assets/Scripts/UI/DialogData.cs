using UnityEngine;
using System.Collections;

public class DialogData
{
    string imageName;
    string text;
    DialogType type;

    public string ImageName
    {
        get {
            return imageName;
        }
    }

    public string Text
    {
        get {
            return text;
        }
    }

    public DialogType Type
    {
        get {
            return type;
        }
    }

    public DialogData(string name, string text, DialogType type)
    {
        this.imageName = name;
        this.text = text;
        this.type = type;
    }
}
