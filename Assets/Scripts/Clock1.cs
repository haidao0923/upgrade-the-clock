using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Clock1 : MonoBehaviour
{
    public bool isHovering = false, isCrit = false;
    float timer;
    Animator anim;
    RectTransform hh, mh, sh;

    Image bg; public Sprite bg0, bg1, bg2, bg3, bg4, bg5;
    Image shi; public Sprite sh1, sh2, sh3, sh4, sh5;
    Image mhi; public Sprite mh1, mh2, mh3, mh4, mh5;
    Image hhi; public Sprite hh1, hh2, hh3, hh4, hh5;

    void Start()
    {
        anim = GetComponent<Animator>();
        
        hh = gameObject.transform.Find("hourHand").GetComponent<RectTransform>();
        mh = gameObject.transform.Find("minuteHand").GetComponent<RectTransform>();
        sh = gameObject.transform.Find("secondHand").GetComponent<RectTransform>();

        bg = GetComponent<Image>();
        shi = gameObject.transform.Find("secondHand").GetComponent<Image>();
        mhi = gameObject.transform.Find("minuteHand").GetComponent<Image>();
        hhi = gameObject.transform.Find("hourHand").GetComponent<Image>();
    }

    void Update()
    {
        if (GameController.data.allUpgrades[1].level >= 20)
        { bg.sprite = bg5; }
        else if (GameController.data.allUpgrades[1].level >= 16)
        { bg.sprite = bg4; }
        else if (GameController.data.allUpgrades[1].level >= 12)
        { bg.sprite = bg3; }
        else if (GameController.data.allUpgrades[1].level >= 8)
        { bg.sprite = bg2; }
        else if (GameController.data.allUpgrades[1].level >= 4)
        { bg.sprite = bg1; }
        else { bg.sprite = bg0; }

        if (isHovering)
        {
            timer += Time.deltaTime;
            if (timer >= (1f / GameController.data.clickPerSecondHold))
            {
                timer = 0;
                click();
            }
        }

        hh.rotation = Quaternion.Euler(0, 180, (float)TimeFunction.GetHour(GameController.data.time) * 30);
        mh.rotation = Quaternion.Euler(0, 180, (float)TimeFunction.GetMinute(GameController.data.time) * 6);
        sh.rotation = Quaternion.Euler(0, 180, (float)TimeFunction.GetSecond(GameController.data.time) * 6);
    }

    public void click()
    {
        GameController.data.tclicks += 1;
        if ((GameController.data.c1SValue >= 4.5f - (GameController.data.allUpgrades[2].level / 2 * GameController.data.allUpgrades[2].currentEffectAmount / 100) 
             && GameController.data.c1SValue <= 5.5f + (GameController.data.allUpgrades[2].level / 2 * GameController.data.allUpgrades[2].currentEffectAmount / 100)) 
             || GameController.data.allUpgrades[2].level >= 9)
        { GameController.data.c1SCombo += 1; }
        else { GameController.data.c1SCombo = 0; }

        if (GameController.data.critRate < 100)
        {
            int random = Random.Range(0, 100);
            random += (int)GameController.data.critRate;
            if (random >= 100)
            { isCrit = true; }
        }
        else { isCrit = true; }

        if (isCrit)
        {
            GameController.data.tickPerClick *= GameController.data.critMultiplier;
        }
        GameController.data.time += GameController.data.tickPerClick;
        GameController.data.totalTime += GameController.data.tickPerClick;
        anim.Play("onClick"); isCrit = false;
    }

    public void onPointerDown()
    {
        isHovering = true;
    }

    public void onPointerUp()
    {
        timer = 0;
        isHovering = false;
    }
}