using UnityEngine;
using System.Collections;

public class PlayerBase : MonoBehaviour
{

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

    PlayerTopDownCamera playerCamera;

    public PlayerTopDownCamera PlayerTopDownCamera {
        get {
            if (playerCamera == null) {
                playerCamera = GetComponent<PlayerTopDownCamera>();
            }

            return playerCamera;
        }
    }
}
