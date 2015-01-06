using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    public Transform targetTransform;
    private NavMeshAgent[] navAgents;

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
