using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BossSummoningRitual : MonoBehaviour
{

    public bool isRacePressed = false;
    Button btn;
    new Text name; Text level, effect, cost;

    void Start()
    {
        btn = gameObject.transform.Find("LevelUP").GetComponent<Button>();
        btn.onClick.AddListener(onPointerDownRaceButton);
        name = gameObject.transform.Find("Name").GetComponent<Text>();
        level = gameObject.transform.Find("Level").GetComponent<Text>();
        effect = gameObject.transform.Find("Effect").GetComponent<Text>();
        cost = gameObject.transform.Find("LevelUP/Text").GetComponent<Text>();
        name.text = GameController.data.BossSummoningRitual.name;

        costAndDamageCalculator();
    }

    void Update()          //change to u1_3
    {
        //increase cost & damage
        costAndDamageCalculator();

        //////////////////////////////////////////////////////////////////////////////// CHECK IF CAN PURCHASE
        if (GameController.data.BossSummoningRitual.level + GameController.data.bulkBuy <= GameController.data.BossSummoningRitual.maxLevel && Boss.bossToken >= GameController.data.BossSummoningRitual.cost)
        {
            btn.interactable = true;
        }
        else { btn.interactable = false; }

        //////////////////////////////////////////////////////////////////////////////////// IF BUTTON PRESSED
        if (isRacePressed)
        {
            Boss.bossToken -= GameController.data.BossSummoningRitual.cost;
            GameController.data.BossSummoningRitual.level += GameController.data.bulkBuy;
            isRacePressed = false;
        }

        //////////////////////////////////////////////////////////////////////////////////// UI
        if (GameController.data.BossSummoningRitual.maxLevel >= 100000)
        {
            GameController.data.BossSummoningRitual.maxLevel = 100000000000000000;
            level.text = "Level " + GameController.data.BossSummoningRitual.level;
        }
        else
        {
            level.text = "Level " + GameController.data.BossSummoningRitual.level + "/" + GameController.data.BossSummoningRitual.maxLevel;
        }

        if (GameController.data.BossSummoningRitual.level < GameController.data.BossSummoningRitual.maxLevel)
        {
            cost.text = "Cost: " + GameController.data.BossSummoningRitual.cost.ToString();
        }
        else { cost.text = "MAX"; cost.color = Color.red; }

        /////////////////////////////////////////////////////////////////////////////////////Only for this
        effect.text = "Reduce \"Summon Bosses\" cooldown by " + (GameController.data.BossSummoningRitual.level * 11).ToString() + "% \n (also work when offline)";
        /////////////////////////////////////////////////////////////////////////////////////Only for this
    }

    private void costAndDamageCalculator()
    {
        long currentCost = (long)((50) * GameController.data.tokenStoreCostMultiplier);
        long finalCost = (long)((50) * GameController.data.tokenStoreCostMultiplier);
        GameController.data.BossSummoningRitual.cost = (long)(GameController.data.bulkBuy * (currentCost + finalCost) / 2); //5 is exponential multiplier

        /////////////////////////////////////////////////////////////////////////////////////Only for this
        /////////////////////////////////////////////////////////////////////////////////////Only for this

    }

    public void onPointerDownRaceButton()
    {
        isRacePressed = true;
    }
}
