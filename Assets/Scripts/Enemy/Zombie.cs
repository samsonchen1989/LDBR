using UnityEngine;
using System.Collections;

public class Zombie : Enemy
{

    public GameObject enemyObject;
    public GameObject crashObject;
    private NavMeshAgent agent;
    private GameObject attackTarget;
    private float attackRange = 1.05f;

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
    }
    
    IEnumerator DestroySelf()
    {
        // Cap collider used to blow up crash objects,
        // disable it after a few frames
        yield return new WaitForSeconds(0.1f);
        this.collider.enabled = false;
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
        agent.enabled = false;
        StartCoroutine(DestroySelf());
    }

    protected override bool Attack()
    {
        if (Vector3.Distance(this.transform.position, attackTarget.transform.position) < attackRange) {
            attackTarget.GetComponent<PlayerState>().GetDamage(attackDamage);
            return true;
        }

        return false;
    }
}
