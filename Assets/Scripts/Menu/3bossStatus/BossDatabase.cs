using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TimeFunction;

public class BossDatabase : MonoBehaviour
{
    public Sprite[] bossSprites;

    void Awake()
    {
        InitializeData(); 
    }

    public void InitializeData()
    {
        GameController.data.allBosses = new Dictionary<int, Boss> { // ID, Name, Image, Skill, Skill Cooldown, Unlock Requirement, Base Lifetime, Quote
            { 0, new Boss(0, "Wizard of Time", bossSprites[0],
              new double[] { 10, 20, 35, 55, 80 },
              new int[] { 0, 0, 0, 0, 0 },
              new double[] {
                  2 * TimeUnit.hour,
                  4 * TimeUnit.day,
                  75 * TimeUnit.day,
                  45 * TimeUnit.year,
                  300d * TimeUnit.year,
              },
              new double[] {
                  2 * TimeUnit.hour,
                  3 * TimeUnit.day,
                  60 * TimeUnit.day,
                  35 * TimeUnit.year,
                  240d * TimeUnit.year,
              },
              new string[] {
                  "I am the Wizard of Time. Some of your clocks seems to be turning faster than normal. Let me fix that for you.",
                  "Hmm... It look like I need to use a stronger spell to slow down the clocks...",
                  "Aha! You are the culprit behind this timely disturbance. Are you unaware of the consequences of your actions? Don't meddle with time.",
                  "The dimensional gate is opening!!! Your world will soon be consume. Stop this playing now, child!",
                  "Very well, I see that you are a promising candidate. Come! I can teach you much more than what you currently know.",
              })
            },

            { 1, new Boss(1, "Ageless Girl", bossSprites[1],
              new double[] { 5, 15, 30, 50, 75 },
              new int[] { 0, 0, 0, 0, 0 },
              new double[] {
                  1 * TimeUnit.day,
                  30 * TimeUnit.day,
                  5 * TimeUnit.year,
                  800d * TimeUnit.year,
                  9999999d * TimeUnit.year,
              },
              new double[] {
                  22 * TimeUnit.hour,
                  20 * TimeUnit.day,
                  4 * TimeUnit.year,
                  600d * TimeUnit.year,
                  9999999d * TimeUnit.year,
              },
              new string[] {
                  "Please stop! My flesh is rotting away!",
                  "My beautiful skin... What have you done?",
                  "My skin take ages to decay, but your hastening of time has stretched thin this blessed body.",
                  "Fool! The effect of accelerating time is also damaging you! Look at how weak and feeble you've become.",
                  "Am I in heaven? My body was somehow restored. Come to my place! I will knit you a hat.",
              })
            },

            { 2, new Boss(2, "Secret Society", bossSprites[2],
              new double[] { 1, 2, 3, 4, 5 },
              new int[] { 0, 0, 0, 0, 0 },
              new double[] {
                  12 * TimeUnit.day,
                  2 * TimeUnit.year,
                  100d * TimeUnit.year,
                  9999999d * TimeUnit.year,
                  9999999d * TimeUnit.year,
              },
              new double[] {
                  8 * TimeUnit.day,
                  300 * TimeUnit.day,
                  80d * TimeUnit.year,
                  9999999d * TimeUnit.year,
                  9999999d * TimeUnit.year,
              },
              new string[] {
                  "???",
                  "???",
                  "???",
                  "???",
                  "???",
              })
            },

            { 3, new Boss(3, "Captain Stealbeard", bossSprites[3],
              new double[] { 15, 30, 30, 1000, 100 }, new int[] { 3, 2, 1, 25, 1 },
              new double[] {
                  240 * TimeUnit.day,
                  15 * TimeUnit.year,
                  2000d * TimeUnit.year,
                  9999999d * TimeUnit.year,
                  9999999d * TimeUnit.year,
              },
              new double[] {
                  130 * TimeUnit.day,
                  15 * TimeUnit.year,
                  1500d * TimeUnit.year,
                  9999999d * TimeUnit.year,
                  9999999d * TimeUnit.year,
              },
              new string[] {
                  "Yaar telling me I have to wait?",
                  "Try to be more like fish foods!",
                  "Ring.Ring.Ring.Ring. Hey! Is this reception bell even working?",
                  "I am going on a big trip today! I need 1000 baits.",
                  "We need ourselves a feast! Nothing is better than watching your workers walk the plank!",
              })
            },


        };
    }
}