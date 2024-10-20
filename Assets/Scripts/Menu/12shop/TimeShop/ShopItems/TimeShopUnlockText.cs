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
        if (Boss.allBossDefeatedCount >= 200)                                   //Unlock new tiers
        {
            text.text = "!FULL STOCK!";
        }
        else if (Boss.allBossDefeatedCount >= 150)                                   //Unlock new tiers
        {
            text.text = "Kill " + (200 - Boss.allBossDefeatedCount).ToString() + " more bosses for Tier 6 items";
        }
        else if (Boss.allBossDefeatedCount >= 110)                                   //Unlock new tiers
        {
            text.text = "Kill " + (150 - Boss.allBossDefeatedCount).ToString() + " more bosses for Tier 5 items";
        }
        else if (Boss.allBossDefeatedCount >= 80)                                   //Unlock new tiers
        {
            text.text = "Kill " + (110 - Boss.allBossDefeatedCount).ToString() + " more bosses for Tier 4 items";
        }
        else if (Boss.allBossDefeatedCount >= 60)                                   //Unlock new tiers
        {
            text.text = "Kill " + (80 - Boss.allBossDefeatedCount).ToString() + " more bosses for Tier 3 items";
        }
        else if (Boss.allBossDefeatedCount >= 40)                                   //Unlock new tiers
        {
            text.text = "Kill " + (60 - Boss.allBossDefeatedCount).ToString() + " more bosses for Tier 2 items";
        }
    }
}
