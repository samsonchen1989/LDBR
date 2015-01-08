using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    private float speed;
    private Vector3 moveDir;
    private float damage;

    public float Damage
    {
        get {
            return damage;
        }
    }

    Transform bulletTrans;

    public void Initialize(float speed, Vector3 moveDir, float damage)
    {
        this.speed = speed;
        this.moveDir = moveDir;
        this.damage = damage;
    }

    // Use this for initialization
    void Start()
    {
        bulletTrans = this.transform;
    }
    
    // Update is called once per frame
    void Update()
    {
        bulletTrans.position = bulletTrans.position + moveDir.normalized * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Zombie") {
            GameObject.Destroy(gameObject);
        }
    }
}
