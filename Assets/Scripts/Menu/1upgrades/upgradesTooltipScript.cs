using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradesTooltipScript : MonoBehaviour
{

    Text text;

	void Start ()
    {
        text = GetComponent<Text>();
	}

	void Update ()
    {
        text.text = "Total TPC: " + GameController.data.tickPerClickDisplay;
	}
}
