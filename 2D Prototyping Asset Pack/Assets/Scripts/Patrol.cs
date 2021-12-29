using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

    public float patrolSpeed;
    public float groundRaycastLength;

    public Transform groundPoint;

    private bool movingLeft = true;


	void Start ()
    {
		
	}

	void Update ()
    {
        transform.Translate(Vector2.left * patrolSpeed * Time.deltaTime);

        RaycastHit2D groundRaycast = Physics2D.Raycast(groundPoint.position, Vector2.down, groundRaycastLength);

        if(groundRaycast.collider == false)
        {
            if(movingLeft == true)
            {
                transform.eulerAngles = new Vector2(0, -180);
                movingLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 0);
                movingLeft = true;
            }
        }
	}
}
