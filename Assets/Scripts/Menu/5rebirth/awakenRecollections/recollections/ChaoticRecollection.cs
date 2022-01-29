using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ChaoticRecollection : MonoBehaviour
{

    public bool isRacePressed = false;
    Button btn;
    new Text name; Text level, effect, currentEffect, cost;
    int hasChaotic;               //only for this one
    string effectListString; List<String> effectList = new List<String>(); 

    void Start()
    {
        btn = gameObject.transform.Find("Awaken").GetComponent<Button>();
        btn.onClick.AddListener(onPointerDownRaceButton);
        name = gameObject.transform.Find("Name").GetComponent<Text>();           
        level = gameObject.transform.Find("Level").GetComponent<Text>();         
        effect = gameObject.transform.Find("Effect").GetComponent<Text>();                   
        currentEffect = gameObject.transform.Find("CurrentEffect").GetComponent<Text>();         
        cost = gameObject.transform.Find("Awaken/Text").GetComponent<Text>();    
        name.text = GameController.data.ChaoticRecollection.name;

        costAndDamageCalculator();
    }

    void Update()          //change to u1_3
    {
        //increase cost & damage
        costAndDamageCalculator();

        //////////////////////////////////////////////////////////////////////////////// CHECK IF CAN PURCHASE
        if (GameController.data.ChaoticRecollection.level + GameController.data.bulkBuy <= GameController.data.ChaoticRecollection.maxLevel && GameController.data.lifeStone >= GameController.data.ChaoticRecollection.cost)
        {
            btn.interactable = true;
        }
        else { btn.interactable = false; }

        //////////////////////////////////////////////////////////////////////////////////// IF BUTTON PRESSED
        if (isRacePressed)                 
        {
            if (GameController.data.ChaoticRecollection.level <= 0)
            {
                GameController.data.lifeStone -= GameController.data.ChaoticRecollection.cost;
                GameController.data.ChaoticRecollection.level = 1;
            }
            else
            {
                GameController.data.lifeStone -= GameController.data.ChaoticRecollection.cost;
                GameController.data.ChaoticRecollection.level += GameController.data.bulkBuy;
            }
            GameController.data.Save();
            isRacePressed = false;
        }

        //////////////////////////////////////////////////////////////////////////////////// UI
        level.text = "Level " + GameController.data.ChaoticRecollection.level + "/" + GameController.data.ChaoticRecollection.maxLevel;

        if (GameController.data.ChaoticRecollection.level <= 0)
        {
            cost.text = "AWAKEN" + "\n Cost: " + GameController.data.ChaoticRecollection.cost.ToString();
        }
        else
        {
            if (GameController.data.ChaoticRecollection.level < GameController.data.ChaoticRecollection.maxLevel)
            {
                cost.text = "IMPROVE" + "\n Cost: " + GameController.data.ChaoticRecollection.cost.ToString();
            }
            else { cost.text = "MAX"; cost.color = Color.red; }
        }

        /////////////////////////////////////////////////////////////////////////////////////Only for this
        effect.text = "Double all stats of " + GameController.data.ChaoticRecollection.level.ToString() + " random workers each rebirth";

        effectListString = "";
        foreach (string str in effectList)
        {
            effectListString += str + ", ";
        }
        if (effectListString.Length >= 2)
        {
            effectListString = effectListString.Substring(0, effectListString.Length - 2);
        }
        currentEffect.text = "Current: " + effectListString;
        /////////////////////////////////////////////////////////////////////////////////////Only for this
    }

    private void costAndDamageCalculator()
    {
        if (GameController.data.ChaoticRecollection.level <= 0) /////////////COST
        {
            GameController.data.ChaoticRecollection.cost = (long)(1 * GameController.data.awakenRecollectionMultiplier);
        }
        else
        {
            GameController.data.ChaoticRecollection.cost = (long)(4 * Mathf.Pow(5, GameController.data.ChaoticRecollection.level) * GameController.data.improveRecollectionMultiplier);
            GameController.data.ChaoticRecollection.cost *= (long)((1 - Mathf.Pow(5, GameController.data.bulkBuy)) / (1 - 5)); //5 is exponential multiplier
        }

        /////////////////////////////////////////////////////////////////////////////////////Only for this
        hasChaotic = 0;
        foreach (Workers workers in GameController.data.unlockedWorkers)
        {
            hasChaotic += workers.isChaotic;
            if (workers.isChaotic == 0)
            {
                GameObject.Find("Canvas/Menu/background/1/2/Workers/" + workers.name).transform.Find("Status/isChaotic").gameObject.SetActive(false);
            }
        }

        for (int i = hasChaotic; i < GameController.data.ChaoticRecollection.level; i++)
        {
            int random = UnityEngine.Random.Range(0, GameController.data.unlockedWorkers.Count);
            GameController.data.unlockedWorkers[random].isChaotic = 1;
            GameObject.Find("Canvas/Menu/background/1/2/Workers/" + GameController.data.unlockedWorkers[random].name).transform.Find("Status/isChaotic").gameObject.SetActive(true);
            if (!effectList.Contains(GameController.data.unlockedWorkers[random].name))
            {
                effectList.Add(GameController.data.unlockedWorkers[random].name);
            }
        }
        if (GameController.data.ChaoticRecollection.level == 0)
        {
            effectList = new List<String>();
        }
        /////////////////////////////////////////////////////////////////////////////////////Only for this

    }

    public void onPointerDownRaceButton()
    {
        isRacePressed = true;
    }
}
