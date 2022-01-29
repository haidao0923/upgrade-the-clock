using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class bossStatusScript : MonoBehaviour
{
    Button btn;
    Image image; public Sprite unPressed, pressed;
    GameObject background, bossStatus;

    void Awake()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(OpenMenu);

        image = GetComponent<Image>();
        background = GameObject.Find("Canvas/Menu/background/1/2");
        bossStatus = background.transform.Find("Boss Status").gameObject; //change this and archives to name

    }

    void Update()
    {
        if (bossStatus.activeSelf) //change worker to button type
        { image.sprite = pressed; }
        else { image.sprite = unPressed; }
    }

    public void OpenMenu()
    {
        foreach (Transform child in background.transform)
        {
            child.gameObject.SetActive(false); bossStatus.SetActive(true);
        }
    }
}