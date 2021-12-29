using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour {

    public Transform backgroundOne;
    public Transform backgroundTwo;

    public Camera gameCam;

    public float currentWidth;
    public float bgWidthGain;

    private bool whichBackground = true;

	void Start ()
    {
	
	}
	
	void Update ()
    {
        if(currentWidth < gameCam.transform.position.x)
        {
            if(whichBackground)
            {
                backgroundOne.localPosition = new Vector2(backgroundOne.localPosition.x + bgWidthGain, 0);
            }
            else
            {
                backgroundTwo.localPosition = new Vector2(backgroundTwo.localPosition.x + bgWidthGain, 0);
            }

            currentWidth += 15;

            whichBackground = !whichBackground;
        }

        if(currentWidth > gameCam.transform.position.x + 15)
        {
            if (whichBackground)
            {
                backgroundTwo.localPosition = new Vector2(backgroundTwo.localPosition.x - bgWidthGain, 0);
            }
            else
            {
                backgroundOne.localPosition = new Vector2(backgroundOne.localPosition.x - bgWidthGain, 0);
            }

            currentWidth -= 15;

            whichBackground = !whichBackground;
        }
	}
}
