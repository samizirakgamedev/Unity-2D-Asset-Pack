using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public int health;
    public int numOfHearts;

    public Image [] heartContainers;
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;

	
	void Start () {
		
	}
	
	
	void Update () {
		
        if(health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < heartContainers.Length; i++)
        {

            if(i < health)
            {
                heartContainers[i].sprite = fullHeartSprite;
            }
            else
            {
                heartContainers[i].sprite = emptyHeartSprite;
            }

            if(i < numOfHearts)
            {
                heartContainers[i].enabled = true;
            }
            else
            {
                heartContainers[i].enabled = false;
            }


        }

	}
}
