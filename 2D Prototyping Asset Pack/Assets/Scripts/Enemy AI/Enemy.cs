using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private IEnemyState currentEnemyState;

    private Rigidbody2D rb2D;

    public static bool facingDefault;

    public GameObject EnemyTarget { get; set; }

    public float movementSpeed = 4.0f;

    public Animator animator;

    public  bool isEnemyFiring = false;

    public GameObject enemyProjectile;

    public Transform projectileSpawnLocation;

    public Vector2 projectileSpawnRotation;

    public float projectileResetTime = 1.0f;

    public string firingBoolName;

    public string patrolBoolName;

    public string chargingBoolName;

	void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        ChangeEnemyState(new PatrolState());
	}
	
	void Update ()
    {
        currentEnemyState.ExecuteState();

        LookAtTarget();
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        currentEnemyState.OnTriggerEnter(col);
    }

    private void LookAtTarget()
    {
        if(EnemyTarget != null)
        {
            float walkDir = EnemyTarget.transform.position.x - transform.position.x;

            if(walkDir < 0 && facingDefault || walkDir > 0 && !facingDefault)
            {
                ChangeDirection();
            }
        }
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
        animator.SetBool("isFiring", false);

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

    public void RangedAttack()
    {
        isEnemyFiring = true;

        Instantiate(enemyProjectile, projectileSpawnLocation.position, Quaternion.Euler(projectileSpawnRotation));

        SoundManager.PlaySound("PlayerFire");

        Invoke("ResetFiring", projectileResetTime);

        animator.SetBool(firingBoolName, true);
        animator.SetBool(chargingBoolName, false);
        animator.SetBool(patrolBoolName, false);

        StartCoroutine(DelayAnimationMethod(0.5f));
    }

    IEnumerator DelayAnimationMethod(float delayTime)
    {
        yield return null;
        yield return new WaitForSeconds(delayTime);
        animator.SetBool(firingBoolName, false);
        animator.SetBool(chargingBoolName, false);
        animator.SetBool(patrolBoolName, true);
        
    }

    void ResetFiring()
    {
        isEnemyFiring = false;
    }

}
