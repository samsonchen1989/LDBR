using UnityEngine;
using System.Collections;

public class PlayerState : PlayerBase
{
    public GameObject playerMod;
    private Material playerMaterial;

    float maxHealth = 100;
    float health = 100;
    bool inVincible = false;

    public float Health {
        get {
            return health;
        }
    }

    public float MaxHelath {
        get {
            return maxHealth;
        }
    }

    public void GetDamage(float damage)
    {
        if (inVincible) {
            // Holy Yeah
            return;
        }

        health -= damage;
        inVincible = true;
        StartCoroutine(InjuredFlash());
    }

    // Override Awake() incase PlayerBase's Awake() called multiple times
    void Awake()
    {
        
    }

    // Use this for initialization
    void Start()
    {
        if (playerMod == null) {
            Debug.LogError("Assign player mod first.");
            return;
        }

        playerMaterial = playerMod.GetComponent<MeshRenderer>().material;
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    IEnumerator InjuredFlash()
    {
        for (int i = 0; i < 3; i++) {
            playerMaterial.color = Color.red;
            //playerMaterial.SetColor("Main Color", Color.red);
            yield return new WaitForSeconds(0.05f);
            playerMaterial.color = Color.white;
            //playerMaterial.SetColor("Main Color", Color.white);
            yield return new WaitForSeconds(0.05f);
        }

        inVincible = false;
    }
}
