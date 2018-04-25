using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static AudioClip playerJump, playerLand, playerFire, playerOutOfAmmo, plusHealth, hitObject, teleport;

    static AudioSource audioSource;

	void Start ()
    {
        playerJump = Resources.Load<AudioClip>("Audio/PlayerJump");
        playerLand = Resources.Load<AudioClip>("Audio/PlayerLand");
        playerFire = Resources.Load<AudioClip>("Audio/PlayerFire");
        playerOutOfAmmo = Resources.Load<AudioClip>("Audio/PlayerOutOfAmmo");
        plusHealth = Resources.Load<AudioClip>("Audio/PlusHealth");
        hitObject = Resources.Load<AudioClip>("Audio/HitObject");
        teleport = Resources.Load<AudioClip>("Audio/Teleport");

        audioSource = GetComponent<AudioSource>();
	}
	

    public static void PlaySound(string audioClipName)
    {
        switch (audioClipName)
        {
            case "PlayerJump":
                audioSource.PlayOneShot(playerJump);
                break;
            case "PlayerLand":
                audioSource.PlayOneShot(playerLand);
                break;
            case "PlayerFire":
                audioSource.PlayOneShot(playerFire);
                break;
            case "PlayerOutOfAmmo":
                audioSource.PlayOneShot(playerOutOfAmmo);
                break;
            case "PlusHealth":
                audioSource.PlayOneShot(plusHealth);
                break;
            case "HitObject":
                audioSource.PlayOneShot(hitObject);
                break;
            case "Teleport":
                audioSource.PlayOneShot(teleport);
                break;
        }
    }
}
