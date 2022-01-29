using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class timeShopButton : MonoBehaviour
{
    public bool isRacePressed = false;
    Button btn; Text text;

    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onPointerDownRaceButton);
        text = transform.Find("Text").GetComponent<Text>();
    }

    void Update()
    {
        if (GameController.data.bossKilled >= 40)
        {
            btn.interactable = true; text.text = "Time Shop";
        }
        else { btn.interactable = false; text.text = "Kill 40 bosses"; }

        if (isRacePressed)  //change worker to button type
        {
            GameObject background = GameObject.Find("Canvas/Menu/background/1/2");
            GameObject shop = background.transform.Find("Shop").gameObject;
            GameObject timeShop = shop.transform.Find("Time Shop").gameObject;
            foreach (Transform child in background.transform)
            {
                child.gameObject.SetActive(false); shop.SetActive(true);
            }
            foreach (Transform child in shop.transform)
            {
                child.gameObject.SetActive(false);
            }
            timeShop.SetActive(true);
            isRacePressed = false;
        }
    }

    public void onPointerDownRaceButton()
    {
        isRacePressed = true;
    }
}