using UnityEngine;
using System.Collections;

public class ObjectFactory : MonoBehaviour
{
    private static ObjectFactory instance;

    // GameObject to spawn
    GameObject bulletPrefab;
    GameObject goldPrefab;

    void Awake()
    {
        if (instance != null) {
            DestroyImmediate(this.gameObject);
        } else {
            instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        bulletPrefab = Resources.Load("pistol_bullet") as GameObject;
        if (bulletPrefab == null) {
            Debug.LogError("Fail to find pistol bullet game object.");
        }

        goldPrefab = Resources.Load("Gold") as GameObject;
        if (goldPrefab == null) {
            Debug.LogError("Fail to find gold game object");
        }
    }

    public static Bullet SpawnBullet(Vector3 position, Vector3 dir, float speed, float damage)
    {
        GameObject go = GameObject.Instantiate(instance.bulletPrefab, position, Quaternion.Euler(0, 0, 0)) as GameObject;
        Bullet bullet = go.GetComponent<Bullet>();
        if (bullet != null) {
            bullet.Initialize(speed, dir, damage);
        }

        return bullet;
    }

    public static void SpawnGold(Vector3 position)
    {
        GameObject.Instantiate(instance.goldPrefab, position, Quaternion.Euler(0, 0, 0));
    }
}
