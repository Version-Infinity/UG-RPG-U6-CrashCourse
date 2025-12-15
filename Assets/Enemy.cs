using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float MoveSpeed = 2.0f;
    [SerializeField] protected string Name = "Enemy";
    [SerializeField] protected int Health = 100;
    

    private void Update()
    {
        MoveAround();
        Attack();
    }   

    private void MoveAround()
    {
    }

    private void Attack()
    {
        // Placeholder for attack logic
        Debug.Log($"{Name} attacks!");
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"{Name} took {damage} damage. Remaining health: {Health}");
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
     
    }
}
