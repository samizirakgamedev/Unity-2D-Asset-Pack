using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

    [SerializeField]
    private Enemy enemy;

    void OnTriggerEnter2D(Collider2D col)
    {
      if(col.tag == "Player")
      {
            enemy.EnemyTarget = col.gameObject;
      }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            enemy.EnemyTarget = null;
        }
    }
}
