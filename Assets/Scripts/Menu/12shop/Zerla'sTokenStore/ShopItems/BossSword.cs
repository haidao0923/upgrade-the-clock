using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BossSword : MonoBehaviour
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
        name.text = GameController.data.BossSword.name;

        costAndDamageCalculator();
    }

    void Update()          //change to u1_3
    {
        //increase cost & damage
        costAndDamageCalculator();

        //////////////////////////////////////////////////////////////////////////////// CHECK IF CAN PURCHASE
        if (GameController.data.BossSword.level + GameController.data.bulkBuy <= GameController.data.BossSword.maxLevel && Boss.bossToken >= GameController.data.BossSword.cost)
        {
            btn.interactable = true;
        }
        else { btn.interactable = false; }

        //////////////////////////////////////////////////////////////////////////////////// IF BUTTON PRESSED
        if (isRacePressed)
        {
            Boss.bossToken -= GameController.data.BossSword.cost;
            GameController.data.BossSword.level += GameController.data.bulkBuy;
            isRacePressed = false;
        }

        //////////////////////////////////////////////////////////////////////////////////// UI
        if (GameController.data.BossSword.maxLevel >= 100000)
        {
            GameController.data.BossSword.maxLevel = 100000000000000000;
            level.text = "Level " + GameController.data.BossSword.level;
        }
        else
        {
            level.text = "Level " + GameController.data.BossSword.level + "/" + GameController.data.BossSword.maxLevel;
        }

        if (GameController.data.BossSword.level < GameController.data.BossSword.maxLevel)
        {
            cost.text = "Cost: " + GameController.data.BossSword.cost.ToString();
        }
        else { cost.text = "MAX"; cost.color = Color.red; }

        /////////////////////////////////////////////////////////////////////////////////////Only for this
        effect.text = "+" + (Mathf.Round(GameController.data.BossSword.effect * 100)).ToString() + "% TPC";
        /////////////////////////////////////////////////////////////////////////////////////Only for this
    }

    private void costAndDamageCalculator()
    {
        long currentCost = (long)(1);
        long finalCost = (long)(1);
        GameController.data.BossSword.cost = (long)(GameController.data.bulkBuy * (currentCost + finalCost) / 2); //5 is exponential multiplier

        /////////////////////////////////////////////////////////////////////////////////////Only for this
        /////////////////////////////////////////////////////////////////////////////////////Only for this

    }

    public void onPointerDownRaceButton()
    {
        isRacePressed = true;
    }
}
