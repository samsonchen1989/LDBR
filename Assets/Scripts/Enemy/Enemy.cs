using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    protected float life;
    protected float attackDamage;

    protected float attackInternal;
    protected float attackRange;
    protected float attackTimer = 0;
    protected bool canAttack = false;

    protected bool isDead;

    protected virtual void Die() {}

    protected virtual bool Attack() { return false; }

    protected virtual void GetDamage(float damage) {}

    public float Life
    {
        get {
            return life;
        }
    }

    public float AttackInternal
    {
        get {
            return attackInternal;
        }
    }

    public float AttackRange
    {
        get {
            return attackRange;
        }
    }

    public float AttackDamage
    {
        get {
            return attackDamage;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet") {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet == null) {
                return;
            }

            GetDamage(bullet.Damage);
            // Destroy bullet
            GameObject.Destroy(collision.gameObject);
        }
    }
}
