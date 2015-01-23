using UnityEngine;
using System.Collections;

public class RobertBoss : Enemy
{

    // Use this for initialization
    void Start()
    {
    
    }

    void InitRobertData()
    {
        isDead = false;
        life = 500;
    }
    
    protected override void Die()
    {
        isDead = true;
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
