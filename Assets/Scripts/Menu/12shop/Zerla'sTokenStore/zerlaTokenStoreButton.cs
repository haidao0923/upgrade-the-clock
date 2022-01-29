using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class zerlaTokenStoreButton : MonoBehaviour
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
        if (GameController.data.bossKilled >= 10)
        {
            btn.interactable = true; text.text = "Zerla's Token Store";
        } else { btn.interactable = false; text.text = "Kill 10 bosses"; }

        if (isRacePressed)  //change worker to button type
        {
            GameObject background = GameObject.Find("Canvas/Menu/background/1/2");
            GameObject shop = background.transform.Find("Shop").gameObject;
            GameObject zerlaTokenStore = shop.transform.Find("Zerla's Token Store").gameObject;
            GameObject zerlaTokenStoreTooltips = zerlaTokenStore.transform.Find("toolTips").gameObject;
            foreach (Transform child in background.transform)
            {
                child.gameObject.SetActive(false);
            }
            shop.SetActive(true);
            foreach (Transform child in shop.transform)
            {
                child.gameObject.SetActive(false);
            }
            zerlaTokenStore.SetActive(true);
            foreach (Transform child in zerlaTokenStore.transform)
            {
                child.gameObject.SetActive(false);
            }
            zerlaTokenStoreTooltips.SetActive(true);
            isRacePressed = false;
        }
    }

    public void onPointerDownRaceButton()
    {
        isRacePressed = true;
    }
}