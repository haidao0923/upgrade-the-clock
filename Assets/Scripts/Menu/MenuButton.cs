using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    private Transform background;
    private GameObject target;
    private Image image;
    public Sprite unPressed, pressed;

    void Start()
    {
        background = GameObject.Find("Canvas/Menu/background/1/2").transform;
        target = background.Find(transform.name).gameObject;
        image = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(OpenMenu);
        if (transform.name == "Upgrades")
        {
            OpenMenu();
        }
    }

    void Update()
    {
        if (!target.activeSelf) //change worker to button type
        {
            image.sprite = unPressed;
        }
    }

    public void OpenMenu()
    {
        foreach (Transform child in background)
        {
            child.gameObject.SetActive(false);
        }
        target.SetActive(true);
        image.sprite = pressed;
    }
}
