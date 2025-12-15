using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer localSpriteRender;
    [SerializeField] private float redColorDuration = .5f;
    [SerializeField] private float redColorTimer;

    private void Awake()
    {
        localSpriteRender = GetComponent<SpriteRenderer>();
        UpdateTimer();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void TakeDamage()
    {
        TurnRed();
    }

    private void TurnRed()
    {
        localSpriteRender.color = Color.red;
    }

    private void Update()
    {
        if (localSpriteRender.color != Color.white)
        {
            redColorTimer -= Time.deltaTime;
            if (redColorTimer <= 0)
            {
                TurnWhite();
                UpdateTimer();
            }
        }
    }

    [ContextMenu("Reset Red Timer")]
    private void UpdateTimer() => redColorTimer = redColorDuration;

    private void TurnWhite()
    {
        localSpriteRender.color = Color.white;
    }   
}
