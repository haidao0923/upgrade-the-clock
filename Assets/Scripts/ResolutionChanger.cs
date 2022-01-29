using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionChanger : MonoBehaviour {

    public GameObject yearText, dayText, clock1, clock1Slider, bosses, menu, menuButtons, offlineGainsDisplay;

	void Awake ()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WebGLPlayer)
        {
            yearText.transform.position = new Vector2(1130,-7.2f + 570);
            yearText.GetComponent<Text>().fontSize = 40;
            dayText.transform.position = new Vector2(1130, -102 + 570);
            dayText.GetComponent<Text>().fontSize = 40;
            clock1.transform.position = new Vector2(1048, 30 + 285);
            clock1Slider.transform.position = new Vector2(1045.6f, 210.6f + 285);
            clock1Slider.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            bosses.transform.position = new Vector3(723, -695 + 285, -10);
            menu.transform.position = new Vector3(-415.4f, -511 + 285, -10);
            menu.transform.localScale = new Vector3(1.8f, 1.96f, 1);
            menuButtons.transform.position = new Vector2(-415, -511 + 285);
            offlineGainsDisplay.transform.position = new Vector2(1030, 243 + 285);
        }
    }

}
