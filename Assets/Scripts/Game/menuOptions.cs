using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuOptions : MonoBehaviour {
    public GameObject canvas;
    public GameObject controlesImage;
    int pulse;
    int pulseS;
    bool canvasB;
    bool controlesB;

	// Use this for initialization
	void Start () {
        pulse = 0;
        canvasB = false;
        controlesB = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void controles()
    {
        if (pulse <= 0 && !canvasB)
        {
            controlesImage.SetActive(true);
            controlesB = true;
            pulse++;
        }
        else
        {
            controlesImage.SetActive(false);
            pulse = 0;
            controlesB = false;
        }
        
    }

    public void sonidos()
    {
        if (pulseS <= 0 && !controlesB)
        {
            canvas.SetActive(true);
            canvasB = true;
            pulseS++;
        }
        else
        {
            canvas.SetActive(false);
            pulseS = 0;
            canvasB = false;
        }
    }
}
