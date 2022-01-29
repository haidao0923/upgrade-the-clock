using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class closeOfflineGainsDisplayButton : MonoBehaviour
{
    public bool isRacePressed = false;
    Button btn;
    GameObject ogd;

    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onPointerDownRaceButton);

        ogd = GameObject.Find("Canvas/Offline Gains Display");
    }

    void Update()
    {
        if (isRacePressed)  //change worker to button type
        {
            ogd.SetActive(false); isRacePressed = false;
        }
    }

    public void onPointerDownRaceButton()
    {
        isRacePressed = true;
    }
}