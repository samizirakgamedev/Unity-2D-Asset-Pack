﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCol : MonoBehaviour {

    public Collider2D otherCol;

	void Awake ()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), otherCol, true);
	}
	
}
