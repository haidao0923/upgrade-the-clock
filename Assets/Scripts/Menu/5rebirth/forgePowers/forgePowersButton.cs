using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class forgePowersButton : MonoBehaviour
{
    public bool isRacePressed = false;
    Button btn;
    GameObject rebirth;

    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onPointerDownRaceButton);
        rebirth = GameObject.Find("Canvas/Menu/background/1/2/Rebirth");
    }

    void Update()
    {
        if (isRacePressed)  //change worker to button type
        {
            GameObject forgePowers = rebirth.transform.Find("forgePowers").gameObject;
            foreach (Transform child in rebirth.transform)
            {
                child.gameObject.SetActive(false); forgePowers.SetActive(true);
            }
            isRacePressed = false;
        }
    }

    public void onPointerDownRaceButton()
    {
        isRacePressed = true;
    }
}