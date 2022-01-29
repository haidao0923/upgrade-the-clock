using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class GameController : MonoBehaviour {

    public static GameController data;
    public DateTime lastTime, currentTime; public long frameTime = 100;
    public double offlineTime; public TimeSpan diff; public long elapsedTime; public long tclicks, tbossclicks;
    public double time;
    public double totalTime;
    public double life = 1, dimension = 1, totalLife = 1, totalDimension = 1;    //current time
    public long bulkBuy
    {
        get
        {
            return bulkBuy;
        }
        set
        {
            bulkBuy = value;
            if (value == 0)
            {
                buyMax = true;
            }  else if (buyMax == true)
            {
                buyMax = false;
            }
        }
    } public bool buyMax;
    public string tickPerClickDisplay, tickPerSecondDisplay;
    public double tickPerClick;
    public double tickPerSecond;
    public double TPCGenericMultiplier, TPSGenericMultiplier;
    public double offlineGain;

    public int clickPerSecondHold; public double critRate; public double critMultiplier;
    public double c1SCombo, c1SComboMultiplier = 0.01f;  public double c1SValue, c1SMaxValue = 10, c1SSpeed = 0.5f;

    public UpgradeManager upgradeManager;
    public Dictionary<int, Upgrade> allUpgrades;
    public Queue<int> unlockedUpgradeQueue = new Queue<int>();
    public double upgradeMaxBonuses;

    public WorkerManager workerManager;
    public Dictionary<int, Worker> allWorkers;
    public Queue<int> unlockedWorkerQueue = new Queue<int>();

    public BossManager bossManager;
    public Dictionary<int, Boss> allBosses;

    public List<Archives> pooledArchives = new List<Archives>(); public List<Archives> unlockedArchives = new List<Archives>();
    public Archives AStarIsBorn;
    public Archives PacifistMoreLikePacifier;

    public long rebirthCount, lifeStone, lifeStoneGain, lifeExpectancy, lifeExpectancyIncrease; public double bossStatGain;

    public List<Recollections> pooledRecollections = new List<Recollections>(); public List<Recollections> unlockedRecollections = new List<Recollections>();
    public double awakenRecollectionMultiplier, improveRecollectionMultiplier;

    public Recollections ChaoticRecollection = new Recollections("Chaotic Recollection", 1, 5, 1);

    public long stolenTokensCount = 0; public double tokenStoreCostMultiplier;
    public ZerlaShop FocusedBossDrain = new ZerlaShop("Focused Boss Drain", 100000);
    public ZerlaShop MultiBossDrain = new ZerlaShop("Multi Boss Drain", 100000);
    public ZerlaShop BossSword = new ZerlaShop("Boss Sword", 100000);
    public ZerlaShop BossShield = new ZerlaShop("Boss Shield", 100000);
    public ZerlaShop BossSummoningRitual = new ZerlaShop("Boss Summoning Ritual", 6);

    public long timeCoins = 60, pengelCount = -1, timeUntilNextPengel, timesAngeredTheSky;
    public List<int> TimeShopBossTokensCost = new List<int>() { 20, 50, 160, 370, 900, 2000 };
    public List<int> TimeShopBossTokensReward = new List<int>() { 50, 160, 590, 1500, 4300, 10000 };

    [DllImport("__Internal")]
    private static extern void SyncFiles();

    void Awake()
    {
        if (data == null)
        {
            DontDestroyOnLoad(gameObject);
            data = this;
        }
        else if (data != this)
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        currentTime = DateTime.Now; lastTime = DateTime.Now;

        //AStarIsBorn = new Archives("A Star Is Born", "atleastWorkers", StarGazer, 100, "TPC", .25f, "Have atleast 100 StarGazer");
        PacifistMoreLikePacifier = new Archives("Pacifist? More Like Pacifier", "special", null, 0, "special", 1, "Don't hit any bosses for 3 minutes");

        //Load();

        pooledArchives.Add(AStarIsBorn);
        pooledArchives.Add(PacifistMoreLikePacifier);

        pooledRecollections.Add(ChaoticRecollection);               //each new
        //////////////////////////////////////////////////////////////////////////////// TIME AWAY CALCULATOR
        elapsedTime = currentTime.Ticks - lastTime.Ticks;
        diff = new TimeSpan(elapsedTime);
        offlineTime = diff.TotalSeconds;
        Debug.Log(diff.TotalSeconds);
        //////////////////////////////////////////////////////////////////////////////// RETURN GAIN CALCULATOR
        foreach (Worker worker in allWorkers.Values)
        {
            returnGainCalculator(worker, offlineTime);
        }

        Boss.summonBossesCount += offlineTimerLoopAmount(ref Boss.currentSummonBossesTimer, Boss.baseSummonBossesTimer);

        Debug.Log(diff.TotalSeconds);
        time += offlineGain;
        totalTime += offlineGain;
    }

    void Start()
    {
        InvokeRepeating("Save", 0.1f, 2f);
        upgradeManager = GetComponent<UpgradeManager>();
        workerManager = GetComponent<WorkerManager>();
        bossManager = GetComponent<BossManager>();
    }

    void Update()
    {
        DateTime now = DateTime.Now;
        long dif = (now.Ticks - currentTime.Ticks) / 100000;
        while(dif >= frameTime)
        {
            oneSecond(); currentTime = DateTime.Now; now = DateTime.Now; dif -= frameTime;
        }

        clickPerSecondHold = 5;
        critRate = 0;
        critMultiplier = 1;

        tickPerClick = 0;
        upgradeMaxBonuses = 1;
        upgradeManager.UpdateUpgrades();
        workerManager.AdjustWorkerStats();
        bossManager.UpdateActiveBosses();

        //////////////////////////////////////////////REBIRTH
        lifeExpectancyIncrease = 20;
        lifeExpectancy = 80 + (long)(life - 1) * lifeExpectancyIncrease;
        lifeStoneGain = (long)(totalTime / 80);

        //////////////////////////////////////////////RECOLLECTIONS
        awakenRecollectionMultiplier = 1;
        improveRecollectionMultiplier = 1;

        //////////////////////////////////////////////ZERLA SHOP
        tokenStoreCostMultiplier = 1;

        FocusedBossDrain.effect = 0.1f * FocusedBossDrain.level;
        MultiBossDrain.effect = 0.05f * MultiBossDrain.level;
        BossSword.effect = 0.03f * BossSword.level;
        BossShield.effect = 0.03f * BossShield.level;

        //////////////////////////////////////////////////////////////////////////////// ADJUST TPC   //each new
        TPCGenericMultiplier = upgradeMaxBonuses * (1 + c1SCombo * c1SComboMultiplier);

        double archiveTPC = 1;
        foreach (Archives archive in unlockedArchives)
        {
            if (archive.bonusType == "TPC")
            {
                archiveTPC += (double)archive.bonus;
            }
        }
        TPCGenericMultiplier *= archiveTPC 
                                /** (1 - AG1.Effects) * (1 - AG2.Effects) */
                                * (1 + BossSword.effect);

        tickPerClick *= TPCGenericMultiplier;

        //////////////////////////////////////////////////////////////////////////////// ADJUST TPS   //each new
        TPSGenericMultiplier = (1 + Boss.bossToken * Boss.bossTokenMultiplier) 
                               /** (1 - WoT1.Effects) * (1 - WoT2.Effects) */
                               * (1 + BossShield.effect);

        //////////////////////////////////////////////////////////////////////////////// HERO TPC TEXT CONVERTER
        tickPerClickDisplay = TimeFunction.ConvertValueToString(tickPerClick);
        tickPerSecondDisplay = TimeFunction.ConvertValueToString(tickPerSecond);
    }






    //////////////////////////////////////////////////////////////////////////////// EVERY SECOND
    void oneSecond()
    {
        workerManager.ReduceWorkerTimer();
        bossManager.SummonBossesTimerCountdown();
        time += tickPerSecond;
        totalTime += tickPerSecond;

        GameObject.Find("Canvas/Menu/background/1/2/Shop/Zerla's Token Store/Shop/Focused Boss Drain").GetComponent<FocusedBossDrain>().oneSecond();
        GameObject.Find("Canvas/Menu/background/1/2/Shop/Zerla's Token Store/Shop/Multi Boss Drain").GetComponent<MultiBossDrain>().oneSecond();

        if (timeUntilNextPengel == 0)
        {
            timeUntilNextPengel = UnityEngine.Random.Range(1, 600);
            pengelCount += 1;
        } timeUntilNextPengel -= 1;
    }

//////////////////////////////////////////////////////////////////////////////// CHECK IF CAN PURCHASE
    public bool canPurchase(double cost)
    {
        return time >= cost;
    }

    //////////////////////////////////////////////////////////////////////////////// PURCHASE COST CALCULATOR
    public void purchaseCalculator(double cost)
    {
        time -= cost;
    }

    public long offlineTimerLoopAmount(ref int time, int maxTime)
    {
        long amount = 0;
        if (offlineTime < time)
        {
            time -= (int)offlineTime;
        }
        else
        {
            offlineTime += maxTime - time;
            amount += (long)Math.Floor(offlineTime / maxTime);
            offlineTime = offlineTime % maxTime;
            time = maxTime;
            time -= (int)offlineTime;
        }
        offlineTime = diff.TotalSeconds;
        return amount;
    }

    //////////////////////////////////////////////////////////////////////////////// RETURN GAIN CALCULATOR
    public void returnGainCalculator(Worker worker, double offlineTime)
    {
        int remainingSeconds = worker.currentTime;
        int workTime = worker.currentWorkTime;
        int exhaustTime = worker.currentExhaustTime;

        if (worker.status == Status.WORKING)
        {
            if (offlineTime < remainingSeconds)
            {
                offlineGain += worker.totalTPS * offlineTime;
                remainingSeconds -= (int) offlineTime;
                worker.currentTime = remainingSeconds;
            }
            else
            {
                offlineGain += worker.totalTPS * remainingSeconds;
                worker.status = Status.EXHAUSTED;
                offlineTime -= remainingSeconds;
                remainingSeconds = exhaustTime;
            }
        }
        if (worker.status == Status.EXHAUSTED)
        {
            if (offlineTime > remainingSeconds)
            {
                worker.status = Status.RESTING;
                worker.currentTime = worker.currentWorkTime;
            }
            else
            {
                remainingSeconds -= (int) offlineTime;
                worker.currentTime = remainingSeconds;
            }
        }
        if (worker.status == Status.RESTING)
        {
            if (remainingSeconds < workTime)
            {
                int gain = workTime / exhaustTime * 2;
                if (gain < 1)
                {
                    gain = 1;
                }
                remainingSeconds += (int) offlineTime * gain;
                if (remainingSeconds > workTime)
                {
                    remainingSeconds = workTime;
                    worker.currentTime = remainingSeconds;
                }
            }
        }
    }



    
    //////////////////////////////////////////////////////////////////////////////// SAVE METHOD
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/v1.dat", FileMode.Create);
        PlayerData data = new PlayerData();

        data.lastTime = DateTime.Now;

        data.AStarIsBornUnlocked = AStarIsBorn.unlocked;
        data.PacifistMoreLikePacifierUnlocked = PacifistMoreLikePacifier.unlocked;

        data.rebirthCount = rebirthCount; data.lifeStone = lifeStone;

        data.stolenTokensCount = stolenTokensCount;
        data.FocusedBossDrain = FocusedBossDrain;
        data.MultiBossDrain = MultiBossDrain;
        data.BossSword = BossSword;
        data.BossShield = BossShield;
        data.BossSummoningRitual = BossSummoningRitual;

        data.timeCoins = timeCoins;

        bf.Serialize(file, data);
        file.Close();
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            SyncFiles();
        }
        Debug.Log("Saved");
    }

    //////////////////////////////////////////////////////////////////////////////// LOAD METHOD
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/v1.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/v1.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);

            lastTime = data.lastTime;

            c1SMaxValue = data.c1SMaxValue; c1SSpeed = data.c1SSpeed;

            AStarIsBorn.unlocked = data.AStarIsBornUnlocked;
            PacifistMoreLikePacifier.unlocked = data.PacifistMoreLikePacifierUnlocked;

            rebirthCount = data.rebirthCount; lifeStone = data.lifeStone;

            checkRecollectionsNull(ref ChaoticRecollection, ref data.ChaoticRecollection);

            stolenTokensCount = data.stolenTokensCount;
            checkZerlaShopNull(ref FocusedBossDrain, ref data.FocusedBossDrain);
            checkZerlaShopNull(ref MultiBossDrain, ref data.MultiBossDrain);
            checkZerlaShopNull(ref BossSword, ref data.BossSword);
            checkZerlaShopNull(ref BossShield, ref data.BossShield);
            checkZerlaShopNull(ref BossSummoningRitual, ref data.BossSummoningRitual);

            timeCoins = data.timeCoins;

            Debug.Log(Application.persistentDataPath);
        }
    }

    public void checkRecollectionsNull(ref Recollections recollection, ref Recollections dataRecollection)
    {
        if (dataRecollection != null)
        {
            recollection = dataRecollection;
        }
    }

    public void checkZerlaShopNull(ref ZerlaShop zerlaShop, ref ZerlaShop dataZerlaShop)
    {
        if (dataZerlaShop != null)
        {
            zerlaShop = dataZerlaShop;
        }
    }

    public void hardReset()
    {
        Destroy(this);
        gameObject.AddComponent<GameController>();
    }
}

[Serializable]
public class Archives
{
    public string name, type, bonusType, effectText; public Worker worker;
    public int typeAmount, unlocked; public float bonus;

    public Archives(string _name, string _type, Worker _worker, int _typeAmount, string _bonusType, float _bonus, string _effectText )
    {
        name = _name; type = _type; worker = _worker; typeAmount = _typeAmount; bonusType = _bonusType; bonus = _bonus; effectText = _effectText;
    }
}

[Serializable]
public class Recollections
{
    public string name;
    public long tier, level, maxLevel, cost;

    public Recollections(string _name, long _tier, long _maxLevel, long _cost)
    {
        name = _name; tier = _tier; maxLevel = _maxLevel; cost = _cost;
    }
}

[Serializable]
public class ZerlaShop
{
    public string name;
    public long level, maxLevel, cost; public float effect;

    public ZerlaShop(string _name, long _maxLevel)
    {
        name = _name; maxLevel = _maxLevel;
    }
}


[Serializable]
class PlayerData
{
    public DateTime lastTime, currentTime;

    public float c1SMaxValue = 10, c1SSpeed = .5f;

    public int AStarIsBornUnlocked, PacifistMoreLikePacifierUnlocked;

    public long rebirthCount, lifeStone;

    public Recollections ChaoticRecollection = new Recollections("Chaotic Recollection", 1, 5, 1);

    public long stolenTokensCount;
    public ZerlaShop FocusedBossDrain = new ZerlaShop("Focused Boss Drain", 100000);
    public ZerlaShop MultiBossDrain = new ZerlaShop("Multi Boss Drain", 100000);
    public ZerlaShop BossSword = new ZerlaShop("Boss Sword", 100000);
    public ZerlaShop BossShield = new ZerlaShop("Boss Shield", 100000);
    public ZerlaShop BossSummoningRitual = new ZerlaShop("Boss Summoning Ritual", 6);

    public long timeCoins;
}