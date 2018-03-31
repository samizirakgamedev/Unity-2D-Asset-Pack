using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static AudioClip playerJump, playerLand;

    static AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {
        playerJump = Resources.Load<AudioClip>("PlayerJump");
        playerLand = Resources.Load<AudioClip>("PlayerLand");

        audioSource = GetComponent<AudioSource>();
	}
	

    public static void PlaySound(string audioClipName)
    {
        Debug.Log("IN");
        switch (audioClipName)
        {
            case "PlayerJump":
                audioSource.PlayOneShot(playerJump);
                break;
            case "PlayerLand":
                audioSource.PlayOneShot(playerLand);
                break;
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
