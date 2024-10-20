using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUIButton : MonoBehaviour
{
    public Boss boss; public GameObject bossInfoDisplay;
    public BossManager bossManager;
    private int activeBossId;
    public bool isHovering, isCrit;
    private double damage;
    private float timer;

    private void OnEnable()
    {
        activeBossId = bossManager.activeBossId;
        BossManager.activeBosses.Add(activeBossId, boss);
        bossManager.activeBossId += 1;
    }

    void Update()
    {
        if (isHovering)
        {
            timer += Time.deltaTime;
            if (timer >= (1f / GameController.data.clickPerSecondHold))
            {
                timer = 0;
                Click();
            }
        }
    }

    private void Click()
    {
        GameController.data.tbossclicks += 1;
        if (GameController.data.critRate < 100)
        {
            int random = Random.Range(0, 100);
            random += (int)GameController.data.critRate;
            if (random >= 100)
            { isCrit = true; }
        }
        else { isCrit = true; }

        damage = GameController.data.tickPerClick;
        if (isCrit)
        {
            damage *= GameController.data.critMultiplier;
        }
        boss.currentLifetime -= damage;
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

    public void CheckDeath()
    {
        if (boss.currentLifetime <= 0)
        {
            Destroy(bossInfoDisplay);
            Destroy(gameObject);
            BossManager.activeBosses.Remove(activeBossId);
        }
    }
}
