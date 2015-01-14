using UnityEngine;
using System.Collections;

public enum RotateDirection
{
    X,
    Y,
    Z
}

public class RotateMove : MonoBehaviour
{
    public float rotateSpeed = 10f;
    public RotateDirection direction;

    float x;
    float y;
    float z;

    // Use this for initialization
    void Start()
    {
        x = this.transform.rotation.eulerAngles.x;
        y = this.transform.rotation.eulerAngles.y;
        z = this.transform.rotation.eulerAngles.z;
    }
    
    // Update is called once per frame
    void Update()
    {
        switch(direction) {
        case RotateDirection.X:
            x += Time.deltaTime * rotateSpeed;
            break;
        case RotateDirection.Y:
            y += Time.deltaTime * rotateSpeed;
            break;
        case RotateDirection.Z:
            z += Time.deltaTime * rotateSpeed;
            break;
        default:
            break;
        }

        this.transform.rotation = Quaternion.Euler(new Vector3(x, y, z));
    }
}
