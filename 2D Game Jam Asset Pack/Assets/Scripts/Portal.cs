using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    public GameObject player;

    public GameObject portal;

    public string playerTag;

    public float teleportDelay;


    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == playerTag & Input.GetButtonDown("Interact"))
        {
            StartCoroutine(Teleport(teleportDelay));
        }
    }

    IEnumerator Teleport(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.transform.position = new Vector2(portal.transform.position.x , portal.transform.position.y);
    }

    void Start () {
		
	}
	

	void Update () {
		
	}
}
