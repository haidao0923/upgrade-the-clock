using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonBossesButton : MonoBehaviour
{
    BossManager bossManager;
    Text count, timer;
    Button btn;

    void Start()
    {
        bossManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BossManager>();
        count = transform.Find("Count").GetComponent<Text>();
        timer = transform.Find("Timer").GetComponent<Text>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    void Update()
    {
        if (Boss.summonBossesCount > 0)
        {
            btn.interactable = true;
        } else
        {
            btn.interactable = false;
        }
        count.text = "Summon Bosses\n(" + Boss.summonBossesCount + " remaining)";
        timer.text = "Time until next: " + TimeFunction.ConvertValueToString(Boss.currentSummonBossesTimer);
    }

    void OnClick()
    {
        bossManager.timer = 0;
        Boss.summonBossesCount -= 1;
    }
}
