using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Dialog : MonoBehaviour {

    public RawImage headImage;
    public Text dialogText;

	// Use this for initialization
	void Start () {
        // Head image can be null in DialogHint
        if (dialogText == null) {
            Debug.LogError("Please assign dialog Text");
            return;
        }
	}

    public void SetImage(string imageName)
    {
        headImage.texture = Resources.Load("head/" + imageName) as Texture;
    }

    public void SetText(string text)
    {
        dialogText.text = text;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
