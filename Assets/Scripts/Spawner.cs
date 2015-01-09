using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject spawnTarget;
    // Spawn number each time
    public int numberEachtime;
    // Total number of spawn target
    public int totalNumber;
    // Time internal between each spawn
    public float timeInternal = 1f;

    private bool enableSpawn = false;
    private float spawnerTimer = 0;
    private int currentNumber = 0;

    public void StartSpawner()
    {
        enableSpawn = true;
        spawnerTimer = timeInternal;
    }

    public void EndSpawn()
    {
        enableSpawn = false;
    }

    // Use this for initialization
    void Start()
    {
        if (spawnTarget == null) {
            Debug.LogError("Please assign spawn target first.");
            return;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (totalNumber > 0 && (currentNumber >= totalNumber)) {
            return;
        }

        if (enableSpawn && spawnerTimer > timeInternal) {
            for (int i = 0; i < numberEachtime; i++) {
                SpawnTarget();
                currentNumber++;
            }

            spawnerTimer = 0;
        }

        spawnerTimer += Time.deltaTime;
    }

    private void SpawnTarget()
    {
        GameObject go = GameObject.Instantiate(spawnTarget, gameObject.transform.position, Quaternion.identity) as GameObject;
        if (go != null) {
            go.GetComponent<NavMeshAgent>().Resume();
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            // Broadcast gameObject spawned
            Messenger<GameObject>.Invoke(MyEventType.SPAWN_TARGET, go);
        }
    }
}
