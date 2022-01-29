using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class workersScript : MonoBehaviour
{
    Button btn;
    Image image; public Sprite unPressed, pressed;
    GameObject background, workers;

    void Awake()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(OpenMenu);

        image = GetComponent<Image>();
        background = GameObject.Find("Canvas/Menu/background/1/2");
        workers = background.transform.Find("Workers").gameObject; //change this and workers to name
    }

    void Update()
    {
        if (!workers.activeSelf)
        {
            image.sprite = unPressed;
        }
    }

    public void OpenMenu()
    {
        foreach (Transform child in background.transform)
        {
            child.gameObject.SetActive(false); workers.SetActive(true);
            image.sprite = pressed;
        }
    }
}