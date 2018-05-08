using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState
{
    private Enemy enemy;

    public void EnterState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void ExecuteState()
    {
        Debug.Log("Im In Ranged State");

        if(enemy.enemyTarget != null)
        {
            enemy.MoveEnemy();
        }

    }

    public void ExitState()
    {
        
    }

    public void OnTriggerEnter(Collider2D col)
    {
        
    }
}
