using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavMeshTarget : MonoBehaviour
{
    public Transform targetTransform;
    private NavMeshAgent[] navAgents;

    void OnEnable()
    {
        Messenger<GameObject>.AddListener(MyEventType.SPAWN_TARGET, SpawnTargetHandler);
    }

    void OnDisable()
    {
        Messenger<GameObject>.RemoveListener(MyEventType.SPAWN_TARGET, SpawnTargetHandler);
    }

    void SpawnTargetHandler(GameObject go)
    {
        if (go.GetComponent<NavMeshAgent>() != null) {
            // Update navAgents
            navAgents = FindObjectsOfType(typeof(NavMeshAgent)) as NavMeshAgent[];
        }
    }

    // Use this for initialization
    void Start()
    {
        if (targetTransform == null) {
            Debug.LogError("Please init navAgents' target first.");
            return;
        }

        navAgents = FindObjectsOfType(typeof(NavMeshAgent)) as NavMeshAgent[];
    }
    
    // Update is called once per frame
    void Update()
    {
        if (navAgents != null) {
            UpdateTarget(targetTransform.position);
        }
    }

    void UpdateTarget(Vector3 position)
    {
        foreach(NavMeshAgent agent in navAgents) {
            if (agent != null && agent.enabled == true) {
                agent.destination = position;
            }
        }
    }
}
