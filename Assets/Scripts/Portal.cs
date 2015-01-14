using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    public Transform targetTrans;

    public bool Enabled
    {
        get {
            return portalEnabled;
        }
    }

    private bool portalEnabled;

    // Use this for initialization
    void Start()
    {
        if (targetTrans == null) {
            Debug.LogError("Please set portal target position.");
        }

        portalEnabled = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (portalEnabled == false) {
            return;
        }

        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.transform.position = targetTrans.position;
        }
    }
}
