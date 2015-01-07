using UnityEngine;
using System.Collections;

public class PlayerBase : MonoBehaviour
{
    private static PlayerBase instance;

    public static PlayerBase Instance
    {
        get {
            if (instance == null) {
                Debug.LogError("Fail to find PlayerBase instance");
            }

            return instance;
        }
    }

    void Awake()
    {
        if (instance != null) {
            Debug.LogError("Only one instance of PlayerBase is allowwed.");
            return;
        }

        instance = this;
    }

    PlayerMoveController controller;

    public PlayerMoveController PlayerMoveController {
        get {
            if (controller == null) {
                controller = GetComponent<PlayerMoveController>();
            }

            return controller;
        }
    }

    PlayerFireController fireController;
    public PlayerFireController PlayerFireController {
        get {
            if (fireController == null) {
                fireController = GetComponent<PlayerFireController>();
            }

            return fireController;
        }
    }

    PlayerEquip equip;
    public PlayerEquip PlayerEquip {
        get {
            if (equip == null) {
                equip = GetComponent<PlayerEquip>();
            }

            return equip;
        }
    }

    PlayerTopDownCamera playerCamera;

    public PlayerTopDownCamera PlayerTopDownCamera {
        get {
            if (playerCamera == null) {
                playerCamera = GetComponent<PlayerTopDownCamera>();
            }

            return playerCamera;
        }
    }

    PlayerState playerState;

    public PlayerState PlayerState {
        get {
            if (playerState == null) {
                playerState = GetComponent<PlayerState>();
            }

            return playerState;
        }
    }
}
