using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock1Slider : MonoBehaviour {

    Slider thisSlider;
    Text comboText;
    Image image; public Sprite cb0, cb1, cb2, cb3, cb4, cb5, cb6, cb7, cb8, cb9;

	void Start ()
    {
        thisSlider = GetComponent<Slider>();
        comboText = gameObject.transform.Find("Combo Text").GetComponent<Text>();
        image = gameObject.transform.Find("Background").GetComponent<Image>();
	}
	
	void Update ()
    {
        switch (GameController.data.allUpgrades[2].level)
        {
            case 0:
                image.sprite = cb0; break;
            case 1:
                image.sprite = cb1; break;
            case 2:
                image.sprite = cb2; break;
            case 3:
                image.sprite = cb3; break;
            case 4:
                image.sprite = cb4; break;
            case 5:
                image.sprite = cb5; break;
            case 6:
                image.sprite = cb6; break;
            case 7:
                image.sprite = cb7; break;
            case 8:
                image.sprite = cb8; break;
            case 9:
                image.sprite = cb9; break;
        }

        if (GameController.data.c1SValue < GameController.data.c1SMaxValue)
        { GameController.data.c1SValue += GameController.data.c1SSpeed; }
        else { GameController.data.c1SValue = 0; }
        thisSlider.value = (float) GameController.data.c1SValue;
        comboText.text = "Combo x" + GameController.data.c1SCombo;
	}
}
