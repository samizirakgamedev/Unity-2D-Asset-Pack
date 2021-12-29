using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : IEnemyState
{
    public void EnterState(Enemy enemy)
    {
        
    }

    public void ExecuteState()
    {
        Debug.Log("Im Chraging");
    }

    public void ExitState()
    {
        
    }

    public void OnTriggerEnter(Collider2D col)
    {
       
    }
}
