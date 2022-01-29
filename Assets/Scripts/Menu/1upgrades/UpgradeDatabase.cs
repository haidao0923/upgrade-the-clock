using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TimeFunction;

public class UpgradeDatabase : MonoBehaviour
{
    public Sprite[] upgradeSprites;

    void Awake()
    {
        InitializeData();
    }

    public void InitializeData()
    {
        GameController.data.allUpgrades = new Dictionary<int, Upgrade> { // name, startingLevel [optional], baseEffectAmount, maxLevel, maxBonus, baseCost, baseCostFactor
            { 0, new Upgrade(0, "Better Clock Hands", upgradeSprites[0], 1,
                             1, 100, 400,
                             30 * TimeUnit.second, 1.2)},
            { 1, new Upgrade(1, "Better Clock Background", upgradeSprites[1],
                             4, 20, 200,
                             3 * TimeUnit.hour, 1.4)},
            { 2, new Upgrade(2, "Better Clock Decoration", upgradeSprites[2],
                             100, 9, 300,
                             8 * TimeUnit.day, 2.2)},
            { 3, new Upgrade(3, "Millenium Clock", upgradeSprites[3],
                             1, 1, 900,
                             1000d * TimeUnit.year, 1)},
        };
    }
}
