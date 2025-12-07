using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D Rb;
    public string PlayerName = "Baller";
    public int Age = 45;
    public float MoveSpeed = 4.35f;
    public bool IsAlive = true;
    [SerializeField] private int score = 0;

    void Update()
    {
        Rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal"), Rb.linearVelocityY);
    }

}