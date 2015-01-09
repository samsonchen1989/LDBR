using UnityEngine;
using System.Collections;

public class AmyDeadDialogHandler : MonoBehaviour
{
    public GameObject zombieSpawner;

    string listenDialog = "dialog2";

    void OnEnable()
    {
        Messenger.AddListener(listenDialog, AmyDeadHandler);
    }

    void OnDisable()
    {
        Messenger.RemoveListener(listenDialog, AmyDeadHandler);
    }

    void Start()
    {
        if (zombieSpawner == null) {
            Debug.LogError("Please assign zombie spawner first.");
            return;
        }
    }

    void AmyDeadHandler()
    {
        PlayerBase.Instance.PlayerEquip.ReplaceSecondWeapon(new Rifle());
        zombieSpawner.GetComponent<Spawner>().StartSpawner();
    }
}
