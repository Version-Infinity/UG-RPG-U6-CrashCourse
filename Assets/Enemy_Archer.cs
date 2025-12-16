using System;
using UnityEngine;

public class Enemy_Archer : Enemy
{
    protected override void Attack()
    {
        ShootArrow();
    }

    private void ShootArrow()
    {
        Debug.Log($"{EnemyName} shoots an arrow!");
    }

}
