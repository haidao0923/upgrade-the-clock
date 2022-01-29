using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class rebirthButton : MonoBehaviour
{
    public bool isRacePressed = false;
    Button btn;
    GameObject rebirth;

    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(onPointerDownRaceButton);
        rebirth = GameObject.Find("Canvas/Menu/background/1/2/Rebirth");
    }

    void Update()
    {
        if (isRacePressed)  //change worker to button type
        {
            GameController.data.seconds = 0; GameController.data.minutes = 0; GameController.data.hours = 0; GameController.data.days = 0; GameController.data.year = 0; GameController.data.epoch = 0;
            GameController.data.tseconds = 0; GameController.data.tminutes = 0; GameController.data.thours = 0; GameController.data.tdays = 0; GameController.data.tyear = 0; GameController.data.tepoch = 0;
            GameController.data.tclicks = 0; GameController.data.tbossclicks = 0;

            GameController.data.c1SCombo = 0;
            GameController.data.u1_1Level = 0; GameController.data.u1_1MaxCounter = 0;
            GameController.data.u1_1TSPC = 0; GameController.data.u1_1TMPC = 0; GameController.data.u1_1THPC = 0; GameController.data.u1_1TDPC = 0; GameController.data.u1_1TYPC = 0; GameController.data.u1_1TEPC = 0;
            GameController.data.u1_2Level = 0; GameController.data.u1_2MaxCounter = 0;
            GameController.data.u1_3Level = 0; GameController.data.u1_3MaxCounter = 0;
            GameController.data.u1_4Level = 0; GameController.data.u1_4MaxCounter = 0;
            GameController.data.u1Max = 0;

            foreach (Workers workers in GameController.data.unlockedWorkers)
            {
                workers.Population = 0;
                workers.TSPS = 0; workers.TMPS = 0; workers.THPS = 0; workers.TDPS = 0; workers.TYPS = 0; workers.TEPS = 0;
                workers.CTS = 0; workers.CTM = 0; workers.CTH = 0; workers.Status = null;

                workers.isChaotic = 0;
            }

            foreach (Bosses bosses in GameController.data.activeBosses)
            {
                bosses.Effects = 0;
            }
            GameController.data.activeBosses = new List<Bosses>();
            GameController.data.pooledBosses = new List<Bosses>();

            GameController.data.lifeStone += GameController.data.lifeStoneGain; GameController.data.rebirthCount += 1;
            GameController.data.life += 1; GameController.data.tlife += 1;

            GameObject awakenRecollections = rebirth.transform.Find("awakenRecollections").gameObject;
            foreach (Transform child in rebirth.transform)
            {
                child.gameObject.SetActive(false);
            }
            transform.parent.gameObject.SetActive(false);
            awakenRecollections.SetActive(true);
            isRacePressed = false;
        }

        if (GameController.data.lifeStoneGain < 1)
        {
            btn.interactable = false;
        } else { btn.interactable = true; }
    }

    public void onPointerDownRaceButton()
    {
        isRacePressed = true;
    }
}