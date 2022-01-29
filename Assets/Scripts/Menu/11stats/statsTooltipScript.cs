using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class statsTooltipScript : MonoBehaviour
{

    Text tdimension, tlife, tepoch, tyear, tdays, thours, tminutes, tseconds,
         tclicks, tbossclicks, bossKilled, WoTKilled, AGKilled, SSKilled;

    void Start()
    {
        tdimension = transform.Find("Total Dimension").GetComponent<Text>();
        tlife = transform.Find("Total Life").GetComponent<Text>();
        tepoch = transform.Find("Total Epoch").GetComponent<Text>();
        tyear = transform.Find("Total Year").GetComponent<Text>();
        tdays = transform.Find("Total Days").GetComponent<Text>();
        thours = transform.Find("Total Hours").GetComponent<Text>();
        tminutes = transform.Find("Total Minutes").GetComponent<Text>();
        tseconds = transform.Find("Total Seconds").GetComponent<Text>();
        tclicks = transform.Find("Total Clicks").GetComponent<Text>();
        tbossclicks = transform.Find("Total Boss Clicks").GetComponent<Text>();
        bossKilled = transform.Find("Total Bosses Killed").GetComponent<Text>();
        WoTKilled = transform.Find("Wizard of Time Killed").GetComponent<Text>();
        AGKilled = transform.Find("Ageless Girl Killed").GetComponent<Text>();
        SSKilled = transform.Find("Secret Society Killed").GetComponent<Text>();

    }

    void Update()
    {
        tdimension.text = "Total Dimension: " + GameController.data.totalDimension;
        tlife.text = "Total Life: " + GameController.data.totalLife;
        tepoch.text = "Total Epoch: " + TimeFunction.GetEpoch(GameController.data.totalTime);
        tyear.text = "Total Years: " + TimeFunction.GetYear(GameController.data.totalTime);
        tdays.text = "Total Days: " + TimeFunction.GetDay(GameController.data.totalTime);
        thours.text = "Total Hours: " + TimeFunction.GetHour(GameController.data.totalTime);
        tminutes.text = "Total Minutes: " + TimeFunction.GetMinute(GameController.data.totalTime);
        tseconds.text = "Total Seconds: " + TimeFunction.GetSecond(GameController.data.totalTime);
        tclicks.text = "Total Clicks: " + GameController.data.tclicks;
        tbossclicks.text = "Total Boss Clicks: " + GameController.data.tbossclicks;
        bossKilled.text = "Total Bosses Defeated: " + Boss.allBossDefeatedCount;
        WoTKilled.text = "Wizard of Time Defeated: " + GameController.data.allBosses[0].GetDefeatedCount();
        AGKilled.text = "Ageless Girl Defeated: " + GameController.data.allBosses[1].GetDefeatedCount();
        SSKilled.text = "Secret Society Defeated: " + GameController.data.allBosses[2].GetDefeatedCount();

    }
}
