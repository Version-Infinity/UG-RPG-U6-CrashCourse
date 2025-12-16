using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 2.0f;
    [SerializeField] protected int health = 100;

    public string EnemyName { get; private set; }

    protected void Update()
    {
        //MoveAround();
        //Attack();
    }

    protected void MoveAround()
    {
    }

    protected virtual void Attack()
    {
        // Placeholder for attack logic
        Debug.Log($"{EnemyName} attacks!");
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{EnemyName} took {damage} damage. Remaining health: {health}");
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{EnemyName} has died.");
        Destroy(gameObject);
    }
}
