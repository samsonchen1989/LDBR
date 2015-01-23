using UnityEngine;
using System.Collections;

public class RobortBossAI : MonoBehaviour {
    public enum ROBORT_STATE
    {
        IDLE,
        ATTACK,
        CHANGE_POS,
        SPAWN_EYE,
        CRASH
    }

    public Transform laserTransform;
    public float rotateSpeed;

    NavMeshAgent agent;
    Transform playerTransform;
    Transform thisTransform;

    ROBORT_STATE activeState = ROBORT_STATE.IDLE;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        playerTransform = GameObject.FindWithTag("Player").transform;
        thisTransform = this.transform;

        SwitchState(activeState);
    }

    void SwitchState(ROBORT_STATE state)
    {
        StopAllCoroutines();

        activeState = state;

        switch(state) {
        case ROBORT_STATE.IDLE:

            break;
        case ROBORT_STATE.ATTACK:
            StartCoroutine(AIAttack());
            break;
        case ROBORT_STATE.CHANGE_POS:
            break;
        case ROBORT_STATE.CRASH:
            break;
        case ROBORT_STATE.SPAWN_EYE:
            break;
        }
    }

    IEnumerator AIAttack()
    {
        agent.Stop();

        while (activeState == ROBORT_STATE.ATTACK) {
            Vector3 dir = new Vector3((playerTransform.position - laserTransform.position).x,
                                      0f, (playerTransform.position - laserTransform.position).z);
            // Rotate toward player first
            Quaternion lookRotation = Quaternion.LookRotation(-dir);
            while (Mathf.Abs(thisTransform.rotation.eulerAngles.y - lookRotation.eulerAngles.y) > 0.5) {
                thisTransform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10);
                yield return null;
            }

            for (int i = 0; i < 5; i++) {
                for (int j = -60; j <= 60; j += 30) {
                    ObjectFactory.SpawnLaser(laserTransform.position, Quaternion.Euler(0, j, 0) * dir, 8f, 5f);
                }

                yield return new WaitForSeconds(0.8f);
            }

            yield return null;
        }
    }
}
