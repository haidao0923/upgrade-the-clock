using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeCoinCountText : MonoBehaviour
{

    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = "x" + GameController.data.timeCoins;
    }
}
