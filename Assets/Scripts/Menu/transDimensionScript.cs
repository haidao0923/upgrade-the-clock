using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class transDimensionScript : MonoBehaviour
{
    public bool isRacePressed = false;
    Button btn;
    Image image; public Sprite unPressed, pressed;
    GameObject background, transDimension;

    void Awake()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(onPointerDownRaceButton);

        image = GetComponent<Image>();
        background = GameObject.Find("Canvas/Menu/background/1/2");
        transDimension = background.transform.Find("Trans-Dimension").gameObject; //change this and archives to name

    }

    void Update()
    {
        if (isRacePressed)  //change worker to button type
        {
            openMenu(); isRacePressed = false;
        }
        if (transDimension.activeSelf) //change worker to button type
        { image.sprite = pressed; }
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
            child.gameObject.SetActive(false); transDimension.SetActive(true);
        }
    }
}