using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rebirthTooltipScript : MonoBehaviour
{

    Text lifeStoneGain, lifeExpectancy, lifeExpectancyIncrease, bossStatGain;

    void Start()
    {
        lifeStoneGain = transform.Find("Buttons/Rebirth/Text").GetComponent<Text>();
        lifeExpectancy = transform.Find("LifeExpectancy").GetComponent<Text>();
        lifeExpectancyIncrease = transform.Find("LifeExpectancyIncrease").GetComponent<Text>();
        bossStatGain = transform.Find("BossStatGain").GetComponent<Text>();
    }

    void Update()
    {
        lifeStoneGain.text = "Rebirth (+" + GameController.data.lifeStoneGain + ")";
        lifeExpectancy.text = "Reborn to gain 1 life stone for every (" + GameController.data.lifeExpectancy + ") total years";
        lifeExpectancyIncrease.text = "(increase by " + GameController.data.lifeExpectancyIncrease + " each rebirth)";
        bossStatGain.text = "Bosses gain " + (GameController.data.bossStatGain * 100).ToString("F0") + "% more health and loot each rebirth";
    }
}
