using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkyScript : MonoBehaviour {

    Image img;
    Text angerTheSkyCount;
    public GameObject pengel1, pengel2;

	void Awake ()
    {
        img = GetComponent<Image>();
        angerTheSkyCount = transform.parent.gameObject.transform.Find("angerTheSkyCount").GetComponent<Text>();
        pengel1 = transform.parent.gameObject.transform.Find("Pengel").gameObject;
        pengel2 = transform.parent.gameObject.transform.Find("Pengel2").gameObject;
    }

    void OnEnable()
    {

        if (GameController.data.pengelCount == 0)
        {
            pengel1.SetActive(false); pengel2.SetActive(false);
        }
        else if (GameController.data.pengelCount == 1)
        {
            int temp = Random.Range(0, 2);
            if (temp == 0)
            {
                pengel1.SetActive(true); pengel2.SetActive(false);
                pengel1.GetComponent<Animator>().Play("PengelAnimation", 0, Random.Range(0f, 1f));
            }
            else if (temp == 1)
            {
                pengel2.SetActive(true); pengel1.SetActive(false);
                pengel2.GetComponent<Animator>().Play("Pengel2Animation", 0, Random.Range(0f, 1f));
            }
        }
        else if (GameController.data.pengelCount >= 2)
        {
            pengel1.SetActive(true); pengel2.SetActive(true);
            pengel1.GetComponent<Animator>().Play("PengelAnimation", 0, Random.Range(0f, 1f));
            pengel2.GetComponent<Animator>().Play("Pengel2Animation", 0, Random.Range(0f, 1f));
        }
    }

    void LateUpdate ()
    {
        angerTheSkyCount.text = GameController.data.timesAngeredTheSky + "/10";
        if (GameController.data.timesAngeredTheSky > 2)
        {
            Color temp = new Color(1, 1 - (float)(GameController.data.timesAngeredTheSky / 10f * 1), 1, 1);
            img.color = temp;
        }
    }

    public void onClick()
    {
        GameController.data.timesAngeredTheSky += 1;
        if (GameController.data.timesAngeredTheSky >= 10)
        {
            GetComponent<Animator>().Play("AngeredSky");
            if (GameController.data.timeCoins > 0)
            {
                GameController.data.timeCoins -= 3;
                if (GameController.data.timeCoins < 0)
                {
                    GameController.data.timeCoins = 0;
                }
            }
            GameController.data.timesAngeredTheSky = 0;
        }
    }
}
