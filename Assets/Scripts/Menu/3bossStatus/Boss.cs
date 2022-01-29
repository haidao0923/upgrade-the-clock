using UnityEngine;

public class Boss
{
    public int id;
    public string name; public Sprite image;
    public double[] unlockRequirement;
    public bool[] unlocked;
    public int level;

    public double[] baseLifetime;
    public double totalLifetime;
    public double currentLifetime;

    public string skillDisplay;
    public double[] baseSkillValue;
    public double skillValue;
    public int[] baseSkillCooldown;
    public double skillCooldown;
    public double timer;

    public double[] baseBounty;
    public double totalBounty;
    public double currentBounty;

    public string[] quote;
    public int spawnScore;
    public int[] defeatedCount;
    public double lifetimeMultiplier, bountyMultiplier, skillMultiplier;

    public static int allBossDefeatedCount;
    public static long bossCount = 1, bossToken, bossTokenGain; public static double bossSpawnTimer = 180, bossTokenMultiplier = 0.05f;
    public static long summonBossesCount = 10;
    public static int baseSummonBossesTimer = 3600, currentSummonBossesTimer; public static string summonBossesTimerText;

    public Boss(Boss otherBoss) : this(otherBoss.id, otherBoss.name, otherBoss.image, otherBoss.baseSkillValue, otherBoss.baseSkillCooldown,
                                       otherBoss.unlockRequirement, otherBoss.baseLifetime, otherBoss.quote)
    {
    }

    public Boss(int id, string name, Sprite image, double[] baseSkillValue, int[] baseSkillCooldown, double[] unlockRequirement,
                double[] baseLifetime, string[] quote)
    {
        this.id = id; this.name = name; this.image = image; this.baseSkillValue = baseSkillValue; this.baseSkillCooldown = baseSkillCooldown;
        this.unlockRequirement = unlockRequirement; this.baseLifetime = baseLifetime; this.quote = quote;
    }

    public int GetDefeatedCount()
    {
        int value = 0;
        foreach (int count in defeatedCount)
        {
            value += count;
        }
        return value;
    }
}