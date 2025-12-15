using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer localSpriteRender;
    [SerializeField] private float redColorDuration = .5f;
    public float CurrentTimeInGame;
    public float LastTimeDamaged;

    private void Awake()
    {
        localSpriteRender = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        CurrentTimeInGame += Time.time;
    }

    public void TakeDamage()
    {
        if (Time.time - LastTimeDamaged < redColorDuration)
            return;
        LastTimeDamaged = Time.time;
        TurnRed();
        Invoke(nameof(TurnWhite), redColorDuration);
    }


    private void TurnRed()
    {
        localSpriteRender.color = Color.red;
    }

    private void TurnWhite()
    {
        localSpriteRender.color = Color.white;
    }   
}
