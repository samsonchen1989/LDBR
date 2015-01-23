using UnityEngine;
using System.Collections;

public class ZombieAI : MonoBehaviour
{
    public enum ZOMBIE_STATE
    {
        PATROL,
        CHASE,
        ATTACK
    }

    public ZOMBIE_STATE activeState = ZOMBIE_STATE.PATROL;

    Transform zombieTransform;
    Transform playerTransform;

    public float chaseDistance = 10.0f;
    public float attackDistance = 1.05f;
    public float patrolDistance = 15.0f;

    float attackInternal;
    float attackDamage;

    NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        zombieTransform = this.transform;
        playerTransform = GameObject.FindWithTag("Player").transform;

        attackInternal = GetComponent<Zombie>().AttackInternal;
        attackDamage = GetComponent<Zombie>().AttackDamage;

        // Set default state
        ChangeState(activeState);
    }

    void OnDisable()
    {
        // Stop all AI Processing
        StopAllCoroutines();
    }

    public void ChangeState(ZOMBIE_STATE state)
    {
        // Stop all AI Processing
        StopAllCoroutines();

        // set new state
        activeState = state;

        switch(activeState) {
        case ZOMBIE_STATE.PATROL:
            StartCoroutine(AIPatrol());
            return;
        case ZOMBIE_STATE.CHASE:
            StartCoroutine(AIChase());
            // Broadcast this event maybe, like play warning sound 
            return;
        case ZOMBIE_STATE.ATTACK:
            StartCoroutine(AIAttack());
            return;
        }
    }

    IEnumerator AIAttack()
    {
        if (agent == null || agent.enabled == false) {
            yield break;
        }

        agent.Stop();

        float elapsedTime = attackInternal;

        while (activeState == ZOMBIE_STATE.ATTACK) {
            elapsedTime += Time.deltaTime;

            float distanceFromPlayer = Vector3.Distance(zombieTransform.position, playerTransform.position);
            //Debug.Log("Attack, distance:" + distanceFromPlayer);
            // Change to patrol state if out of chase range
            if (distanceFromPlayer > chaseDistance) {
                ChangeState(ZOMBIE_STATE.PATROL);
                yield break;
            }

            // Change to chase state if not close enough
            if (distanceFromPlayer > attackDistance) {
                ChangeState(ZOMBIE_STATE.CHASE);
                yield break;
            }

            if (elapsedTime >= attackInternal) {
                elapsedTime = 0;
                PlayerBase.Instance.PlayerState.GetDamage(attackDamage);
            }

            yield return null;
        }
    }

    IEnumerator AIChase()
    {
        if (agent == null || agent.enabled == false) {
            yield break;
        }

        agent.Stop();

        while (activeState == ZOMBIE_STATE.CHASE) {
            if (agent != null && agent.enabled) {
                // Target is the player
                agent.SetDestination(playerTransform.position);
                // Chase speed is relatively high
                agent.speed = 1.5f;
            }

            float distanceFromPlayer = Vector3.Distance(zombieTransform.position, playerTransform.position);
            //Debug.Log("Chase, distance:" + distanceFromPlayer);
            // If close enough, attack
            if (distanceFromPlayer < attackDistance) {
                ChangeState(ZOMBIE_STATE.ATTACK);
                yield break;
            }

            // If out of chase distance, return to patrol state
            if (distanceFromPlayer > chaseDistance) {
                ChangeState(ZOMBIE_STATE.PATROL);
                yield break;
            }

            // Wait until next frame
            yield return null;
        }
    }

    IEnumerator AIPatrol()
    {
        if (agent == null || agent.enabled == false) {
            yield break;
        }

        agent.Stop();

        while (activeState == ZOMBIE_STATE.PATROL) {
            // Get random destination on map
            Vector3 randomPosition = Random.insideUnitSphere * patrolDistance;
            randomPosition += zombieTransform.position;

            // Get nearest valid position
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPosition, out hit, patrolDistance, 1);

            if (agent != null && agent.enabled) {
                // Set destination
                agent.SetDestination(hit.position);
                // Portal speed is low
                agent.speed = 0.5f;
            }

            // Set distance range between object and destination to classify "arrived"
            float arrivalDistance = 0.5f;
            // Set timeout before new path is generated(5 seconds)
            float timeOut = 10f;
            // Elapsed time
            float elapsedTime = 0;

            // Wait until enemy reached destination or time out, then get new position
            while (Vector3.Distance(zombieTransform.position, hit.position) > arrivalDistance && (elapsedTime < timeOut)) {
                elapsedTime += Time.deltaTime;

                float distanceFromPlayer = Vector3.Distance(zombieTransform.position, playerTransform.position);
                //Debug.Log("Patrol, distance:" + distanceFromPlayer);
                // Check if should enter chase state
                if (distanceFromPlayer < chaseDistance) {
                    ChangeState(ZOMBIE_STATE.CHASE);
                    yield break;
                }

                yield return null;
            }

            yield return null;
        }
    }
}
