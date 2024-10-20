using UnityEngine;
using System.Collections.Generic;

public class BossManager : MonoBehaviour
{
    public double timer;
    private int totalSpawnScore;
    public BossUILayout bossUILayout;
    public GameObject[][] bossButtons;
    private int maxBossLevel = 5;
    private int[] bossLevelBias = new int[5];

    public int activeBossId;
    public static Dictionary<int, Boss> activeBosses = new Dictionary<int, Boss>();

    void Awake()
    {
        timer = Boss.bossSpawnTimer;
    }

    void Update()
    {
        UnlockBossAndCalculateSpawnChance();
        SpawnBoss();
    }

    public void SummonBossesTimerCountdown()
    {
        TimeFunction.Countdown(ref Boss.currentSummonBossesTimer);
        if (Boss.currentSummonBossesTimer <= 0)
        {
            Boss.summonBossesCount += 1;
            Boss.currentSummonBossesTimer = Boss.baseSummonBossesTimer;
        }
    }

    public void UnlockBossAndCalculateSpawnChance()
    {
        totalSpawnScore = 0;
        double tempTime = GameController.data.time;
        foreach (Boss boss in GetUnlockedBosses())
        {
            for (int i = 0; i < boss.unlockRequirement.Length; i++)
            {
                if (tempTime >= boss.unlockRequirement[i])
                {
                    if (!boss.unlocked[i])
                    {
                        boss.unlocked[i] = true;
                        timer = 0;
                    }
                    totalSpawnScore += boss.spawnScore + bossLevelBias[i];
                }

            }
        }
    }

    public void SpawnBoss()
    {
        if (totalSpawnScore > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            for (int bossCount = 0; bossCount < Boss.bossCount; bossCount++)
            {
                int random = Random.Range(0, totalSpawnScore);
                foreach (Boss boss in GetUnlockedBosses())
                {
                    for (int i = 0; i < boss.unlocked.Length; i++)
                    {
                        if (boss.unlocked[i])
                        {
                            random -= boss.spawnScore + bossLevelBias[i];
                            if (random < 0)
                            {
                                timer = Boss.bossSpawnTimer;
                                bossUILayout.CreatePrefab(InitializeBossStats(boss.id, i + 1));
                            }
                        }

                    }
                }
            }
        }
    }

    public Boss InitializeBossStats(int bossId, int level)
    {
        Boss boss = new Boss(GameController.data.allBosses[bossId]);
        boss.level = level;

        boss.lifetimeMultiplier = 1;
        boss.skillMultiplier = 1;
        boss.bountyMultiplier = 1;

        boss.totalLifetime = boss.baseLifetime[boss.level - 1] * boss.lifetimeMultiplier;
        boss.currentLifetime = boss.totalLifetime;

        boss.skillValue = boss.baseSkillValue[boss.level - 1] * boss.skillMultiplier;
        boss.skillCooldown = boss.baseSkillCooldown[boss.level - 1];
        boss.timer = boss.skillCooldown;

        boss.baseBounty[boss.level - 1] = boss.totalLifetime * 1.5f * boss.bountyMultiplier;
        boss.totalBounty = boss.baseBounty[boss.level - 1];
        boss.currentBounty = boss.totalBounty;

        return boss;
    }

    public List<Boss> GetUnlockedBosses()
    {
        List<Boss> unlockedBosses = new List<Boss>();
        double tempTime = GameController.data.time;
        foreach (Boss boss in GameController.data.allBosses.Values)
        {
            if (tempTime >= boss.unlockRequirement[0])
            {
                unlockedBosses.Add(boss);
            }
        }
        return unlockedBosses;
    }

    public void UpdateActiveBosses()
    {
        foreach (Boss boss in activeBosses.Values)
        {
            if (boss.currentLifetime <= 0)
            {
                GameController.data.allBosses[boss.id].defeatedCount[boss.level - 1] += 1; ; Boss.allBossDefeatedCount += 1;
                GameController.data.time += boss.currentBounty;
                Boss.bossToken += Boss.bossTokenGain;
            }

            if (boss.skillCooldown > 0)
            {
                boss.timer -= Time.deltaTime;
                if (boss.timer <= 0)
                {
                    boss.timer = boss.skillCooldown;
                }
            }

            switch (boss.id)
            {
                case 0:
                    boss.skillDisplay = "- " + boss.skillValue + "% TPS";
                    GameController.data.tickPerSecond *= 1 - boss.skillValue;
                    break;
                case 1:
                    boss.skillDisplay = "- " + boss.skillValue + "% TPC";
                    GameController.data.tickPerClick *= 1 - boss.skillValue;
                    break;
                case 2:
                    //boss.skillDisplay = "-" + boss.skillValue + "% TPS";
                    break;
                case 3:
                    boss.skillDisplay = "Lose " + boss.skillValue + " random workers";
                    //if (boss.timer == 0)
                    break;

            }
        }
    }
}