using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class awakenRecollectionsButton : MonoBehaviour
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
            GameObject awakenRecollections = rebirth.transform.Find("awakenRecollections").gameObject;
            foreach (Transform child in rebirth.transform)
            {
                child.gameObject.SetActive(false); awakenRecollections.SetActive(true);
            }
            isRacePressed = false;
        }
    }

    public void onPointerDownRaceButton()
    {
        isRacePressed = true;
    }
}