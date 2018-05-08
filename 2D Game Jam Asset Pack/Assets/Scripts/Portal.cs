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
            StartCoroutine(Teleport(teleportDelay, player));
        }
    }

    IEnumerator Teleport(float delay, GameObject beingTeleported)
    {
        CharacterMovement2D playerMovement = beingTeleported.GetComponent<CharacterMovement2D>();
        playerMovement.enabled = false;
        SoundManager.PlaySound("Teleport");
        yield return new WaitForSeconds(delay);
        beingTeleported.transform.position = new Vector2(portal.transform.position.x , portal.transform.position.y);
        playerMovement.enabled = true;
    }
}
