using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpikedEnemyBehaviour : MonoBehaviour {

    public int health;

    public GameObject [] possibleDrops;

    public Transform dropSpawn;

    public void TakeDamage(int damage)
    {
        health -= damage;

        //SoundManager.PlaySound("DamageEnemy");

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        System.Random dropRNG = new System.Random();
        int rngValue = dropRNG.Next(0, 100);

        Debug.Log(rngValue);

        if(rngValue < 50)
        {
            
        }

        if (rngValue >= 50)
        {    
            Instantiate(possibleDrops[0], transform.position, Quaternion.Euler(0,0,0));
        }


    }

}
