using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public GameObject enemyObject;
    public GameObject crashObject;

    public float life = 30f;

    private NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        if (enemyObject == null || crashObject == null) {
            Debug.LogError("Fail to find Enemy game object");
            return;
        }

        agent = GetComponent<NavMeshAgent>();
        if (agent == null) {
            Debug.LogError("Fail to find enemy navMeshAgent");
            return;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(2.5f);
        GameObject.Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet") {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet == null) {
                return;
            }

            life -= bullet.Damage;
            // Destroy bullet
            GameObject.Destroy(collision.gameObject);

            if (life <= 0) {
                // Show crash objects and hide enemy body
                crashObject.SetActive(true);
                GameObject.Destroy(enemyObject);
                // Disable NavMeshAgent first
                agent.enabled = false;
                // After 2.5s destroy self
                StartCoroutine(DestroySelf());
            }
        }
    }
}
