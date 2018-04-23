using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {


    public int healthValue;

    public Vector2 spawnDropForce;

    public float forceTime;

    public string bulletTagName;

    public string playerTagName;

    private Rigidbody2D rb2D;

    private bool hasBeenDropped;

	// Use this for initialization
	void Start () {

        rb2D = GetComponent<Rigidbody2D>();

        hasBeenDropped = false;

        StartCoroutine(DropHeart(forceTime));
		
	}

    IEnumerator DropHeart(float forceTime)
    {
        yield return new WaitForSeconds(forceTime);
        hasBeenDropped = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {      
        if(collision.tag == bulletTagName)
        {
            SoundManager.PlaySound("HitObject");

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if(collision.tag == playerTagName)
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.health = playerHealth.health + healthValue;

            SoundManager.PlaySound("PlusHealth");

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update ()
    {
		if(hasBeenDropped == false)
        {
            rb2D.AddForce(spawnDropForce);
        }
	}
}
