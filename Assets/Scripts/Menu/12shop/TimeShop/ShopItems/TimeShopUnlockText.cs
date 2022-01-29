using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeShopUnlockText : MonoBehaviour
{

    public Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (GameController.data.bossKilled >= 200)                                   //Unlock new tiers
        {
            text.text = "!FULL STOCK!";
        }
        else if (GameController.data.bossKilled >= 150)                                   //Unlock new tiers
        {
            text.text = "Kill " + (200 - GameController.data.bossKilled).ToString() + " more bosses for Tier 6 items";
        }
        else if (GameController.data.bossKilled >= 110)                                   //Unlock new tiers
        {
            text.text = "Kill " + (150 - GameController.data.bossKilled).ToString() + " more bosses for Tier 5 items";
        }
        else if (GameController.data.bossKilled >= 80)                                   //Unlock new tiers
        {
            text.text = "Kill " + (110 - GameController.data.bossKilled).ToString() + " more bosses for Tier 4 items";
        }
        else if (GameController.data.bossKilled >= 60)                                   //Unlock new tiers
        {
            text.text = "Kill " + (80 - GameController.data.bossKilled).ToString() + " more bosses for Tier 3 items";
        }
        else if (GameController.data.bossKilled >= 40)                                   //Unlock new tiers
        {
            text.text = "Kill " + (60 - GameController.data.bossKilled).ToString() + " more bosses for Tier 2 items";
        }
    }
}
