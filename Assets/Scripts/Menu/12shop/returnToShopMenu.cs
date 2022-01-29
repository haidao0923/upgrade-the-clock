using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class returnToShopMenu : MonoBehaviour
{
    public bool isRacePressed = false;
    Button btn;
    GameObject shop;

    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onPointerDownRaceButton);
        shop = GameObject.Find("Canvas/Menu/background/1/2/Shop");
    }

    void Update()
    {
        if (isRacePressed)  //change worker to button type
        {
            GameObject toolTips = shop.transform.Find("toolTips").gameObject;
            foreach (Transform child in shop.transform)
            {
                child.gameObject.SetActive(false);
            }
            toolTips.SetActive(true);
            isRacePressed = false;
        }
    }

    public void onPointerDownRaceButton()
    {
        isRacePressed = true;
    }
}