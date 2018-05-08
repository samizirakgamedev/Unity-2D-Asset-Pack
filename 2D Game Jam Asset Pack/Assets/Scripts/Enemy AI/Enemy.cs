using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private IEnemyState currentEnemyState;

    private Rigidbody2D rb2D;

    private bool facingDefault;

    public GameObject enemyTarget { get; set; }

    public float movementSpeed = 4.0f;

    public Animator animator;

	// Use this for initialization
	void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        ChangeEnemyState(new PatrolState());
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentEnemyState.ExecuteState();
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        currentEnemyState.OnTriggerEnter(col);
    }

    public void ChangeEnemyState(IEnemyState newState)
    {
        if(currentEnemyState != null)
        {
            currentEnemyState.ExitState();
        }

        currentEnemyState = newState;

        currentEnemyState.EnterState(this);
    }

    public void MoveEnemy()
    {
        animator.SetBool("enemyInRange", false);

        transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));

    }

    public Vector2 GetDirection()
    {
        return facingDefault ? Vector2.right : Vector2.left;
    }

    public void ChangeDirection()
    {
        facingDefault = !facingDefault;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
