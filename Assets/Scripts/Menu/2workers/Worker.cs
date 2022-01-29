using UnityEngine;

public enum Status
{
    WORKING, RESTING, EXHAUSTED, NONE
}

public class Worker
{
    public int id;
    public string name, quote, costDisplay, tpsDisplay, totalTPSDisplay, currentTimeDisplay, workTimeDisplay, exhaustTimeDisplay;
    public Status status; public Sprite image;
    public long population, populationCap, buyAmount;
    public bool unlocked
    {
        get
        {
            return unlocked;
        }
        set
        {
            if (unlocked == false && value == true)
            {
                GameController.data.unlockedWorkerQueue.Enqueue(id);
            }
            unlocked = value;
        }
    }

    public double baseCost;
    public double currentCost;
    public double costMultiplier = 1;

    public double baseTPS;
    public double currentTPS;
    public double totalTPS;
    public double tpsMultiplier = 1;

    public int currentTime;
    public int baseWorkTime;
    public int currentWorkTime;
    public int baseExhaustTime;
    public int currentExhaustTime;
    public double workTimeMultiplier = 1, exhaustTimeMultiplier = 1;

    public int isChaotic;

    public Worker(int id, string name, Sprite image,
                  double baseCost,
                  double baseTPS,
                  int baseWorkTime,
                  int baseExhaustTime,
                  string quote)
    {
        this.id = id; this.name = name; this.image = image;
        this.baseCost = baseCost;
        this.baseTPS = baseTPS;
        this.baseWorkTime = baseWorkTime;
        this.baseExhaustTime = baseExhaustTime;
        this.quote = quote;
    }
}
