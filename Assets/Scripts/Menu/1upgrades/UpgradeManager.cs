using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public void UpdateUpgrades()
    {
        Upgrade upgrade = GameController.data.allUpgrades[0]; //Change this
        double total = (upgrade.currentEffectAmount + upgrade.currentEffectAmount * upgrade.level) * upgrade.level / 2;
        upgrade.currentCostFactor = Math.Pow(upgrade.baseCostFactor, upgrade.level);
        upgrade.effectDisplay = "TPC: " + TimeFunction.ConvertValueToString(upgrade.currentEffectAmount * upgrade.level) + "\nTotal TPC: " + TimeFunction.ConvertValueToString(total); //Change this
        GameController.data.tickPerClick += total;

        upgrade = GameController.data.allUpgrades[1];
        upgrade.effectDisplay = "- " + (int)upgrade.currentEffectAmount + "% Combo Speed" + "\nTotal: - " + (int)(upgrade.level * upgrade.currentEffectAmount) + "%";
        GameController.data.c1SSpeed -= upgrade.currentEffectAmount / 200;

        upgrade = GameController.data.allUpgrades[2];
        upgrade.effectDisplay = "+ " + (int)upgrade.currentEffectAmount + "% Combo Zone" + "\nTotal: + " + (int)(upgrade.level * upgrade.currentEffectAmount) + "%";

        upgrade = GameController.data.allUpgrades[3];
        upgrade.effectDisplay = "The millenium clock will turn once more.";

        Upgrade.maxedEverything = true;
        foreach (Upgrade i in GameController.data.allUpgrades.Values)
        {
            i.currentEffectAmount = i.baseEffectAmount;
            i.currentCostFactor = i.baseCostFactor;
            i.currentCost = i.baseCost * i.currentCostFactor;

            if (i.isMax)
            {
                GameController.data.upgradeMaxBonuses *= (i.maxBonus + 100) / 100;
            } else
            {
                Upgrade.maxedEverything = false;
            }
        }
        if (Upgrade.maxedEverything)
        {
            GameController.data.upgradeMaxBonuses *= 2;
        }
    }
}
