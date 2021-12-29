using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyFollow2D : MonoBehaviour {

    public Camera gameCam;

    public Vector3 objOffset;

    private GameObject skyObj;

    void Start ()
    {
        skyObj = GetComponent<GameObject>();
	}
	

	void Update ()
    {
        Vector3 objPosition = objOffset;

        objPosition.x += gameCam.transform.position.x;

        //gameCam.transform.position = objPosition;
	}
}
