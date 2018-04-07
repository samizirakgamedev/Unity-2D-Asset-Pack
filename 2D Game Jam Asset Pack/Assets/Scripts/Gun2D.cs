using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun2D : MonoBehaviour {

    public GameObject bulletToFire;

    private Transform characterPosition;

    public Transform bulletSpawnPoint;

    [SerializeField]
    [Tooltip("")]
    private string firingBoolName;

    [SerializeField]
    private float gunFireInterval = 1.5f;

    [SerializeField]
    private Vector3 bulletSpawnRotation = new Vector3(0,0,0);

    [SerializeField]
    private bool gunHasSound = false;

    private Animator characterAnimator;

    private static bool isFiring = false;

    public static bool isFlipped = false;

	void Start ()
    {
        characterPosition = GetComponent<Transform>();

        characterAnimator = GetComponent<Animator>();
	}

    void ResetGun()
    {
        isFiring = false;
    }

    void ShootGun()
    {
        isFiring = true;
        
        Instantiate(bulletToFire, bulletSpawnPoint.position, Quaternion.Euler(bulletSpawnRotation));

        if (gunHasSound == true)
            SoundManager.PlaySound("PlayerFire");

        Invoke("ResetGun", gunFireInterval);

        characterAnimator.SetBool(firingBoolName, true);

        StartCoroutine(DelayAnimationMethod(0.5f));

        
    }

    IEnumerator DelayAnimationMethod(float delayTime)
    {
        yield return null;
        yield return new WaitForSeconds(delayTime);
        characterAnimator.SetBool(firingBoolName, false);
    }

	void Update () {

        if (Input.GetMouseButtonDown(0))
            if(!isFiring)
                ShootGun();

        if (characterPosition.localScale.x < 0 && !isFiring)
            isFlipped = true;
        else if (characterPosition.localScale.x > 0 && !isFiring)
            isFlipped = false;
       
	}
}
