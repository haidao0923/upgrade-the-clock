using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FocusedBossDrain : MonoBehaviour
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
        name.text = GameController.data.FocusedBossDrain.name;

        costAndDamageCalculator();
    }

    void Update()          //change to u1_3
    {
        //increase cost & damage
        costAndDamageCalculator();

        //////////////////////////////////////////////////////////////////////////////// CHECK IF CAN PURCHASE
        if (GameController.data.FocusedBossDrain.level + GameController.data.bulkBuy <= GameController.data.FocusedBossDrain.maxLevel && GameController.data.bossTokens >= GameController.data.FocusedBossDrain.cost)
        {
            btn.interactable = true;
        }
        else { btn.interactable = false; }

        //////////////////////////////////////////////////////////////////////////////////// IF BUTTON PRESSED
        if (isRacePressed)
        {
            GameController.data.bossTokens -= GameController.data.FocusedBossDrain.cost;
            GameController.data.FocusedBossDrain.level += GameController.data.bulkBuy;
            isRacePressed = false;
        }

        //////////////////////////////////////////////////////////////////////////////////// UI
        if (GameController.data.FocusedBossDrain.maxLevel >= 100000)
        {
            GameController.data.FocusedBossDrain.maxLevel = 100000000000000000;
            level.text = "Level " + GameController.data.FocusedBossDrain.level;
        }
        else
        {
            level.text = "Level " + GameController.data.FocusedBossDrain.level + "/" + GameController.data.FocusedBossDrain.maxLevel;
        }

        if (GameController.data.FocusedBossDrain.level < GameController.data.FocusedBossDrain.maxLevel)
        {
            cost.text = "Cost: " + GameController.data.FocusedBossDrain.cost.ToString();
        }
        else { cost.text = "MAX"; cost.color = Color.red; }

        /////////////////////////////////////////////////////////////////////////////////////Only for this
        effect.text = "Deal " + (Mathf.Round(GameController.data.FocusedBossDrain.effect * 100)).ToString() + "% TPC every second to the first boss that appear";
        /////////////////////////////////////////////////////////////////////////////////////Only for this
    }

    private void costAndDamageCalculator()
    {
        long currentCost = (long)((GameController.data.FocusedBossDrain.level + 1) * GameController.data.tokenStoreCostMultiplier);
        long finalCost = (long)(((GameController.data.FocusedBossDrain.level + GameController.data.bulkBuy - 1) + 1) * GameController.data.tokenStoreCostMultiplier);
        GameController.data.FocusedBossDrain.cost = (long)(GameController.data.bulkBuy * (currentCost + finalCost) / 2); //5 is exponential multiplier

        /////////////////////////////////////////////////////////////////////////////////////Only for this
        /////////////////////////////////////////////////////////////////////////////////////Only for this

    }

    public void oneSecond()
    {
        if (GameController.data.activeBosses.Count > 0 && GameController.data.FocusedBossDrain.level > 0)
        {
            GameController.data.activeBosses[0].CHS -= GameController.data.SPC * GameController.data.FocusedBossDrain.effect;
            GameController.data.activeBosses[0].CHM -= GameController.data.MPC * GameController.data.FocusedBossDrain.effect;
            GameController.data.activeBosses[0].CHH -= GameController.data.HPC * GameController.data.FocusedBossDrain.effect;
            GameController.data.activeBosses[0].CHD -= GameController.data.DPC * GameController.data.FocusedBossDrain.effect;
            GameController.data.activeBosses[0].CHY -= GameController.data.YPC * GameController.data.FocusedBossDrain.effect;
            GameController.data.activeBosses[0].CHE -= GameController.data.EPC * GameController.data.FocusedBossDrain.effect;
        }
    }

    public void onPointerDownRaceButton()
    {
        isRacePressed = true;
    }
}
