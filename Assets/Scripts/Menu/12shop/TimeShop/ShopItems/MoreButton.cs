using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoreButton : MonoBehaviour {

    GameObject optionB; Text text;

    void Start()
    {
        optionB = transform.parent.transform.Find("OptionB").gameObject;
        text = transform.Find("More/Text").GetComponent<Text>();
    }

    void Update()
    {
        if (optionB.activeSelf == true)
        {
            text.text = "Less...";
        }
        else
        {
            text.text = "More...";
        }
    }

	public void onClick()
    {
        if (optionB.activeSelf == true)
        {
            optionB.SetActive(false);
        }
        else
        {
            optionB.SetActive(true);
        }
    }
}
