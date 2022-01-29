using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MultiBossDrain : MonoBehaviour
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
        name.text = GameController.data.MultiBossDrain.name;

        costAndDamageCalculator();
    }

    void Update()          //change to u1_3
    {
        //increase cost & damage
        costAndDamageCalculator();

        //////////////////////////////////////////////////////////////////////////////// CHECK IF CAN PURCHASE
        if (GameController.data.MultiBossDrain.level + GameController.data.bulkBuy <= GameController.data.MultiBossDrain.maxLevel && GameController.data.bossTokens >= GameController.data.MultiBossDrain.cost)
        {
            btn.interactable = true;
        }
        else { btn.interactable = false; }

        //////////////////////////////////////////////////////////////////////////////////// IF BUTTON PRESSED
        if (isRacePressed)
        {
            GameController.data.bossTokens -= GameController.data.MultiBossDrain.cost;
            GameController.data.MultiBossDrain.level += GameController.data.bulkBuy;
            isRacePressed = false;
        }

        //////////////////////////////////////////////////////////////////////////////////// UI
        if (GameController.data.MultiBossDrain.maxLevel >= 100000)
        {
            GameController.data.MultiBossDrain.maxLevel = 100000000000000000;
            level.text = "Level " + GameController.data.MultiBossDrain.level;
        }
        else
        {
            level.text = "Level " + GameController.data.MultiBossDrain.level + "/" + GameController.data.MultiBossDrain.maxLevel;
        }

        if (GameController.data.MultiBossDrain.level < GameController.data.MultiBossDrain.maxLevel)
        {
            cost.text = "Cost: " + GameController.data.MultiBossDrain.cost.ToString();
        }
        else { cost.text = "MAX"; cost.color = Color.red; }

        /////////////////////////////////////////////////////////////////////////////////////Only for this
        effect.text = "Deal " + (Mathf.Round(GameController.data.MultiBossDrain.effect * 100)).ToString() + "% TPC every second to all bosses";
        /////////////////////////////////////////////////////////////////////////////////////Only for this
    }

    private void costAndDamageCalculator()
    {
        long currentCost = (long)((GameController.data.MultiBossDrain.level * 3 + 3) * GameController.data.tokenStoreCostMultiplier);
        long finalCost = (long)(((GameController.data.MultiBossDrain.level + GameController.data.bulkBuy - 1) * 3 + 3) * GameController.data.tokenStoreCostMultiplier);
        GameController.data.MultiBossDrain.cost = (long)(GameController.data.bulkBuy * (currentCost + finalCost) / 2); //5 is exponential multiplier

        /////////////////////////////////////////////////////////////////////////////////////Only for this
        /////////////////////////////////////////////////////////////////////////////////////Only for this

    }

    public void oneSecond()
    {
        if (GameController.data.activeBosses.Count > 0 && GameController.data.MultiBossDrain.level > 0)
        {
            foreach (Bosses boss in GameController.data.activeBosses)
            {
                boss.CHS -= GameController.data.SPC * GameController.data.MultiBossDrain.effect;
                boss.CHM -= GameController.data.MPC * GameController.data.MultiBossDrain.effect;
                boss.CHH -= GameController.data.HPC * GameController.data.MultiBossDrain.effect;
                boss.CHD -= GameController.data.DPC * GameController.data.MultiBossDrain.effect;
                boss.CHY -= GameController.data.YPC * GameController.data.MultiBossDrain.effect;
                boss.CHE -= GameController.data.EPC * GameController.data.MultiBossDrain.effect;
            }
        }
    }

    public void onPointerDownRaceButton()
    {
        isRacePressed = true;
    }
}
