using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {

    protected bool triggerEnable = false;

    public bool TiggerEnable
    {
        get {
            return triggerEnable;
        }
        
        set {
            triggerEnable = value;
        }
    }

    public bool showOnce = false;

    public bool ShowOnce
    {
        get {
            return showOnce;
        }
    }

    public bool needSpaceButton = false;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("in, tag:" + other.gameObject.tag);
        if (other.gameObject.tag == "PlayerTrigger") {
            triggerEnable = true;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        //Debug.Log("out, tag:" + other.gameObject.tag);
        if (other.gameObject.tag == "PlayerTrigger") {
            triggerEnable = false;
        }
    }
}
