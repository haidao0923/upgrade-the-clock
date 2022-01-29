using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulkBuy : MonoBehaviour
{
    Button selectButton; Image image;
    Text text;
    void Start()
    {
        selectButton = GetComponent<Button>();
        selectButton.onClick.AddListener(OnClick);
        image = GetComponent<Image>();
        text = transform.Find("Text").GetComponent<Text>();
    }

    void Update()
    {
        if (GameController.data.bulkBuy.ToString() == gameObject.name)
        {
            image.color = new Color(0, 0.8113208f, 0.1084905f);
        }
        else { image.color = new Color(0.3537736f, 0.635441f, 1); }
        if (GameController.data.bulkBuy > 0)
        {
            text.text = transform.name + "x";
        } else
        {
            text.text = "BUY MAX";
        }
    }

    void OnClick()
    {

        GameController.data.bulkBuy = int.Parse(gameObject.name);
    }
}
