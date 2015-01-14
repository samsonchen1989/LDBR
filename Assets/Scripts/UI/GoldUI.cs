using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoldUI : MonoBehaviour
{
    Text goldText;

    // Use this for initialization
    void Start()
    {
        goldText = GetComponent<Text>();
        if (goldText == null) {
            Debug.LogError("Fail to find gold text component");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        goldText.text = PlayerBase.Instance.PlayerState.Gold + " G";
    }
}
