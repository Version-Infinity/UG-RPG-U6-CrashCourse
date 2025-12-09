using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Trapdoor : MonoBehaviour
{
    [SerializeField] private bool isOpen = true;
    [SerializeField] private int framesToStayOpen = 10;
    private int openFrames = 0;
    private Collider2D trapdoorCollider;

    private void Awake()
    {
        trapdoorCollider = GetComponent<Collider2D>();
        trapdoorCollider.enabled = !isOpen;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (isOpen)
            CheckTrapdoor();

    }

    private void CheckTrapdoor()
    {
        if (isOpen && ++openFrames >= framesToStayOpen)
        {
            trapdoorCollider.enabled = true;
            isOpen = false;
        }
    }

}
