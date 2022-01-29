using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class workersTooltipScript : MonoBehaviour
{

    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = "Total TPS: " + GameController.data.TPS;
    }
}
