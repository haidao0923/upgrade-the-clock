using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class upgradesScript : MonoBehaviour
{
    public bool isRacePressed = false;
    Button btn;
    Image image; public Sprite unPressed, pressed;
    GameObject background, upgrades;
    GameObject ph;

    void Awake()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(onPointerDownRaceButton);

        image = GetComponent<Image>();
        background = GameObject.Find("Canvas/Menu/background/1/2");
        upgrades = background.transform.Find("Upgrades").gameObject; //change this and archives to name

        ph = GameObject.Find("Canvas/Menu/background/1/2/Upgrades/Placeholder");

    }

    void Start()
    {
        openMenu();
    }

    void Update()
    {
        if (isRacePressed)  //change worker to button type
        {
            openMenu(); isRacePressed = false;
        }

        if (upgrades.activeSelf) //change worker to button type
        {
            image.sprite = pressed;
            if (GameController.data.u1_4Unlocked)
            { ph.SetActive(false); } else { ph.SetActive(true); }
            if (GameController.data.u1_1MaxCounter == 1 && GameController.data.u1_2MaxCounter == 1 && GameController.data.u1_3MaxCounter == 1 && GameController.data.u1_4MaxCounter == 1)
            { GameController.data.u1Max = 1; }
        }
        else { image.sprite = unPressed; }
    }

    public void onPointerDownRaceButton()
    {
        isRacePressed = true;
    }

    public void openMenu()
    {
        foreach (Transform child in background.transform)
        {
            child.gameObject.SetActive(false); upgrades.SetActive(true);
        }
    }
}