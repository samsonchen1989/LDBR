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

    // If true, broadcast a message when dialog is over
    public bool broadcast = false;

    public bool Broadcast
    {
        get {
            return broadcast;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerTrigger") {
            triggerEnable = true;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerTrigger") {
            triggerEnable = false;
        }
    }
}
