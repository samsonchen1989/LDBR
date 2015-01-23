using UnityEngine;
using System.Collections;

public class Zombie : Enemy
{

    public GameObject enemyObject;
    public GameObject crashObject;
    private NavMeshAgent agent;
    private GameObject attackTarget;
    private ZombieAI ai;

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

        ai = GetComponent<ZombieAI>();
        if (ai == null) {
            Debug.LogError("Did this zombie forget to take an AI?");
            return;
        }

        attackTarget = GameObject.FindWithTag("Player");
        if (attackTarget == null) {
            Debug.LogError("Fail to find attack target");
            return;
        }

        InitZombieData();
    }

    // Todo, read from config file
    void InitZombieData()
    {
        isDead = false;
        life = 30f;
        attackDamage = 20f;
        attackInternal = 3f;
        attackRange = 1.05f;
    }
    
    IEnumerator DestroySelf()
    {
        // Cap collider used to blow up crash objects,
        // disable it after a few frames
        yield return new WaitForSeconds(0.1f);
        this.collider.enabled = false;
        ObjectFactory.SpawnGold(this.transform.position);
        yield return new WaitForSeconds(5f);
        GameObject.Destroy(gameObject);
    }

    protected override void Die()
    {
        isDead = true;

        // Show crash objects and hide enemy body
        crashObject.SetActive(true);
        GameObject.Destroy(enemyObject);
        // Disable NavMeshAgent first
        ai.enabled = false;
        agent.enabled = false;
        StartCoroutine(DestroySelf());
    }

    protected override void GetDamage(float damage)
    {
        // Get damage response, color flash
        GetComponent<PingPongShaderColor>().Play();

        life -= damage;
        if (life <= 0) {
            Die();
        }
    }
}
