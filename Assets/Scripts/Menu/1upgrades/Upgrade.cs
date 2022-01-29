using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    public int id;
    public string name, costDisplay, effectDisplay;
    public Sprite image;
    public int level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
            if (level == maxLevel && !isMax)
            {
                isMax = true;
            } else if (level != maxLevel && isMax)
            {
                isMax = false;
            }
        }
    }
    public bool isMax; public static bool maxedEverything;
    public int maxLevel;
    public double baseEffectAmount, currentEffectAmount, maxBonus;
    public double baseCost, currentCost;
    public double baseCostFactor, currentCostFactor;
    public bool unlocked
    {
        get
        {
            return unlocked;
        }
        set
        {
            if (unlocked == false && value == true)
            {
                GameController.data.unlockedUpgradeQueue.Enqueue(id);
            }
            unlocked = value;
        }
    }

    public Upgrade(int id, string name, Sprite image,
                   double baseEffectAmount, int maxLevel, double maxBonus,
                   double baseCost, double baseCostFactor) : this(id, name, image, 0, baseEffectAmount, maxLevel, maxBonus, baseCost, baseCostFactor)
    {
    }

    public Upgrade(int id, string name, Sprite image, int startingLevel,
               double baseEffectAmount, int maxLevel, double maxBonus,
               double baseCost, double baseCostFactor)
    {
        this.id = id;
        this.name = name;
        this.image = image;
        this.level = startingLevel;
        this.baseEffectAmount = baseEffectAmount;
        this.maxLevel = maxLevel;
        this.maxBonus = maxBonus;
        this.baseCost = baseCost;
        this.baseCostFactor = baseCostFactor;
    }
}
