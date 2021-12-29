using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private Enemy enemy;

    public void EnterState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void ExecuteState()
    {
        Debug.Log("Im Patroling");

        enemy.MoveEnemy();

        if(enemy.EnemyTarget != null)
        {
            enemy.ChangeEnemyState(new RangedState());
        }
    }

    public void ExitState()
    {
        
    }

    public void OnTriggerEnter(Collider2D col)
    {
       if(col.tag == "Edge")
        {
            enemy.ChangeDirection();
        }
    }
}
