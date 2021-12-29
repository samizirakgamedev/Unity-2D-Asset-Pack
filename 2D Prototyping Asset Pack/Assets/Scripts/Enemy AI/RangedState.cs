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

        if(enemy.isEnemyFiring == false)
            enemy.RangedAttack();

        if(enemy.EnemyTarget != null)
        {
            if(enemy.isEnemyFiring == false)
                enemy.MoveEnemy();
        }
        else
        {
            enemy.ChangeEnemyState(new PatrolState());
        }
    }

    public void ExitState()
    {
        
    }

    public void OnTriggerEnter(Collider2D col)
    {
        
    }
}
