using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifeStoneText : MonoBehaviour
{

    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = "x" + GameController.data.lifeStone;
    }
}
