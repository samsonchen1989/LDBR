using UnityEngine;
using System.Collections;

public enum FireDir
{
    UP,
    RIGHT,
    DOWN,
    LEFT,
    NONE
}

public class PlayerFireController : PlayerBase
{
    int lastInputKey = 0;
    int currentInputKey = 0;

    FireDir fireDirection;
    public FireDir FireDirection {
        get {
            return fireDirection;
        }
    }

    Vector3 fireVector = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        fireDirection = FireDir.NONE;
    }
    
    // Update is called once per frame
    void Update()
    {
        int keyUp = Input.GetKey(KeyCode.UpArrow) ? 1 : 0;
        int keyRight = Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
        int keyDown = Input.GetKey(KeyCode.DownArrow) ? 1 : 0;
        int keyLeft = Input.GetKey(KeyCode.LeftArrow) ? 1 : 0;

        fireDirection = GetFireDirection(keyUp, keyRight, keyDown, keyLeft);
        if (fireDirection != FireDir.NONE) {
            switch(fireDirection) {
            case FireDir.UP:
                fireVector = new Vector3(0, 0, 1f);
                break;
            case FireDir.RIGHT:
                fireVector = new Vector3(1f, 0, 0);
                break;
            case FireDir.DOWN:
                fireVector = new Vector3(0, 0, -1f);
                break;
            case FireDir.LEFT:
                fireVector = new Vector3(-1f, 0, 0);
                break;
            }

            PlayerEquip.Shoot(gameObject.transform.position + new Vector3(fireVector.x * 0.8f, 0.5f, fireVector.z * 0.8f), fireVector);
        }
    }

    FireDir GetFireDirection(int keyUp, int keyRight, int keyDown, int keyLeft)
    {
        FireDir dir = FireDir.NONE;

        currentInputKey = (keyUp << 3) | (keyRight << 2) | (keyDown << 1) | (keyLeft);

        if (currentInputKey == lastInputKey) {
            dir = fireDirection;
            return dir;
        }

        if (currentInputKey == 0) {
            lastInputKey = currentInputKey;
            // FireDir.NONE
            return dir;
        } else if (currentInputKey == 1) {
            dir = FireDir.LEFT;
            lastInputKey = currentInputKey;
            return dir;
        } else if (currentInputKey == 2) {
            dir = FireDir.DOWN;
            lastInputKey = currentInputKey;
            return dir;
        } else if (currentInputKey == 4) {
            dir = FireDir.RIGHT;
            lastInputKey = currentInputKey;
            return dir;
        } else if (currentInputKey == 8) {
            dir = FireDir.UP;
            lastInputKey = currentInputKey;
            return dir;
        }

        // If two or more buttons are pressed, last input key is the fire direction
        int inputDiff = (lastInputKey ^ currentInputKey) & currentInputKey;
        switch (inputDiff) {
        case 1:
            dir = FireDir.LEFT;
            break;
        case 2:
            dir = FireDir.DOWN;
            break;
        case 4:
            dir = FireDir.RIGHT;
            break;
        case 8:
            dir = FireDir.UP;
            break;
        default:
            break;
        }

        // Refresh last input key
        lastInputKey = currentInputKey;

        return dir;
    }
}
