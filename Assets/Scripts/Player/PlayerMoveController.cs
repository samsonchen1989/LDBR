using UnityEngine;
using System.Collections;

enum MoveDir
{
    UP,
    UP_RIGHT,
    RIGHT,
    DOWN_RIGHT,
    DOWN,
    DOWN_LEFT,
    LEFT,
    UP_LEFT,
    STILL
}

public enum FaceDir
{
    UP,
    RIGHT,
    DOWN,
    LEFT
}

public class PlayerMoveController : PlayerBase
{
    Transform playerTrans;
    Rigidbody phyBody;
    MoveDir moveDir = MoveDir.STILL;
    FaceDir faceDir = FaceDir.DOWN;

    float inputHorizon;
    float inputVertical;
    int inputKey = 0;

    #region const value

    const float MoveSpeed = 3f;
    
    #endregion

    public FaceDir FaceDirection
    {
        get {
            return faceDir;
        }
    }

    // Override Awake() incase PlayerBase's Awake() called multiple times
    void Awake()
    {
        
    }

    // Use this for initialization
    void Start()
    {
        phyBody = GetComponent<Rigidbody>();
        if (phyBody == null) {
            Debug.LogError("Fail to find player's rigidbody.");
        }

        playerTrans = this.transform;
    }
    
    // Update is called once per frame
    void Update()
    {
        int keyA = Input.GetKey(KeyCode.A) ? 1 : 0;
        int keyD = Input.GetKey(KeyCode.D) ? 1 : 0;
        int keyW = Input.GetKey(KeyCode.W) ? 1 : 0;
        int keyS = Input.GetKey(KeyCode.S) ? 1 : 0;

        moveDir = GetMoveDirection(keyA, keyD, keyW, keyS);

        // Rotate according to move direction
        PlayerRotate();
    }

    void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerRotate()
    {
        // Check fire direction first
        FireDir fireDir = PlayerFireController.FireDirection;
        if (fireDir != FireDir.NONE) {
            switch(fireDir)
            {
            case FireDir.DOWN:
                playerTrans.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                faceDir = FaceDir.DOWN;
                break;
            case FireDir.LEFT:
                playerTrans.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                faceDir = FaceDir.LEFT;
                break;
            case FireDir.RIGHT:
                playerTrans.transform.rotation = Quaternion.Euler(0f, 270f, 0f);
                faceDir = FaceDir.RIGHT;
                break;
            case FireDir.UP:
                playerTrans.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                faceDir = FaceDir.UP;
                break;
            }

            return;
        }

        // If no fire, rotate depending on move direction
        switch (moveDir)
        {
        case MoveDir.DOWN:
        case MoveDir.DOWN_LEFT:
        case MoveDir.DOWN_RIGHT:
            playerTrans.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            faceDir = FaceDir.DOWN;
            break;
        case MoveDir.UP:
        case MoveDir.UP_LEFT:
        case MoveDir.UP_RIGHT:
            playerTrans.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            faceDir = FaceDir.UP;
            break;
        case MoveDir.LEFT:
            playerTrans.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            faceDir = FaceDir.LEFT;
            break;
        case MoveDir.RIGHT:
            playerTrans.transform.rotation = Quaternion.Euler(0f, 270f, 0f);
            faceDir = FaceDir.RIGHT;
            break;
        default:
            break;
        }
    }

    // Get player move direction from keyboard input
    MoveDir GetMoveDirection(int keyA, int keyD, int keyW, int keyS)
    {
        MoveDir dir = MoveDir.STILL;

        if (keyA == 1 && keyD == 1) {
            keyA = 0;
            keyD = 0;
        }

        if (keyW == 1 && keyS == 1) {
            keyW = 0;
            keyS = 0;
        }

        //inputKey = (((((keyA << 1) | keyD) << 1) | keyW) << 1) | keyS;
        inputKey = (keyA << 3) | (keyD << 2) | (keyW << 1) | keyS;

        switch(inputKey) {
        case 0:
            // 0x0000
            dir = MoveDir.STILL;
            break;
        case 1:
            // 0x0001, "S" pressed
            dir = MoveDir.DOWN;
            break;
        case 2:
            // 0x0010, "W" pressed
            dir = MoveDir.UP;
            break;
        case 4:
            // 0x0100, "D" pressed
            dir = MoveDir.RIGHT;
            break;
        case 8:
            // 0x1000, "A" pressed
            dir = MoveDir.LEFT;
            break;
        case 5:
            // 0x0101, "D" and "S" pressed
            dir = MoveDir.DOWN_RIGHT;
            break;
        case 6:
            // 0x0110, "D" and "W" pressed
            dir = MoveDir.UP_RIGHT;
            break;
        case 9:
            // 0x1001, "A" and "S" pressed
            dir = MoveDir.DOWN_LEFT;
            break;
        case 10:
            // 0x1010, "A" and "W" pressed
            dir = MoveDir.UP_LEFT;
            break;
        default:
            break;
        }

        return dir;
    }

    void PlayerMove()
    {
        Vector3 moveVector = Vector3.zero;

        switch (moveDir)
        {
        case MoveDir.DOWN:
            moveVector = new Vector3(0, 0, -1f);
            break;
        case MoveDir.UP:
            moveVector = new Vector3(0, 0, 1f);
            break;
        case MoveDir.LEFT:
            moveVector = new Vector3(-1f, 0, 0);
            break;
        case MoveDir.RIGHT:
            moveVector = new Vector3(1f, 0, 0);
            break;
        case MoveDir.UP_LEFT:
            moveVector = new Vector3(-1f, 0, 1f);
            break;
        case MoveDir.UP_RIGHT:
            moveVector = new Vector3(1f, 0, 1f);
            break;
        case MoveDir.DOWN_LEFT:
            moveVector = new Vector3(-1f, 0, -1f);
            break;
        case MoveDir.DOWN_RIGHT:
            moveVector = new Vector3(1f, 0, -1f);
            break;
        case MoveDir.STILL:
            phyBody.velocity = Vector3.zero;
            return;
        default:
            return;
        }

        phyBody.MovePosition(phyBody.position + moveVector * MoveSpeed * Time.deltaTime);
    }
}
