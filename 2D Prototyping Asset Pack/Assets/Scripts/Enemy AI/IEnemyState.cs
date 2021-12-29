using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState {

    void EnterState(Enemy enemy);
    void ExitState();
    void ExecuteState();
    void OnTriggerEnter(Collider2D col);
}
