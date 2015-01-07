using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    protected float life;
    protected float attackDamage;

    protected float attackInternal;

    protected float attackTimer = 0;
    protected bool canAttack = false;

    protected bool isDead;

    protected virtual void Die() {}

    protected virtual bool Attack() { return false; }

    void Update()
    {
        if (isDead)
        {
            return;
        }

        if (!canAttack) {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackInternal) {
                canAttack = true;
            }
        }

        if (canAttack) {
            if (Attack()) {
                canAttack = false;
                attackTimer = 0f;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet") {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet == null) {
                return;
            }

            life -= bullet.Damage;
            // Destroy bullet
            GameObject.Destroy(collision.gameObject);

            if (life <= 0) {
                Die();
            }
        }
    }
}
