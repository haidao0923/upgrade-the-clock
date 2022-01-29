using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UpgradeUIPrefab : MonoBehaviour
{
    Button levelUpButton;
    Text scriptedName, maxBonus, level, cost, effect;
    Image image;

    public Upgrade upgrade;

    void Start()
    {
        scriptedName = transform.Find("Name").GetComponent<Text>();
        maxBonus = transform.Find("Bonus").GetComponent<Text>();
        image = transform.Find("Image").GetComponent<Image>();
        levelUpButton = transform.Find("LevelUp").GetComponent<Button>();
        levelUpButton.onClick.AddListener(ClickedLevelUp);
        level = transform.Find("Level").GetComponent<Text>();            //FIND MENU ITEM
        cost = transform.Find("LevelUp/Cost").GetComponent<Text>();      //FIND MENU ITEM
        effect = transform.Find("Effect").GetComponent<Text>();         //FIND MENU ITEM
    }

    void Update()          //change to u1_2
    {
        upgrade.costDisplay = TimeFunction.ConvertValueToString(upgrade.currentCost);

        scriptedName.text = upgrade.name;
        maxBonus.text = "Max Bonus: + " + upgrade.maxBonus.ToString() + "% TPC";
        image.sprite = upgrade.image;
        level.text = "Level " + upgrade.level + " / " + upgrade.maxLevel;
        effect.text = upgrade.effectDisplay;
        if (upgrade.level < upgrade.maxLevel && GameController.data.canPurchase(upgrade.currentCost))
        {
            levelUpButton.interactable = true;
        }
        else { levelUpButton.interactable = false; }
        if (!upgrade.isMax)
        {
            cost.text = upgrade.costDisplay; cost.color = Color.green;
        }
        else { cost.text = "MAX"; cost.color = Color.red; }
    }

    public void ClickedLevelUp()
    {
        GameController.data.purchaseCalculator(upgrade.currentCost);
        upgrade.level += 1;
        GameController.data.Save();
    }
}
