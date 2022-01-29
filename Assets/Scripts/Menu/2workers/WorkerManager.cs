using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    private double workerTPS;

    void Start()
    {
        /*foreach (Workers workers in pooledWorkers)
        {
            returnGainCalculator(ref workers.Status, ref workers.CTS, ref workers.CTM, ref workers.CTH, ref workers.WTS, ref workers.WTM, ref workers.WTH, ref workers.ETS, ref workers.ETM, ref workers.ETH, workers.TSPS, workers.TMPS, workers.THPS, workers.TDPS, workers.TYPS, workers.TEPS);
            offlineTimeVariable = (double)diff.TotalSeconds;
        }*/
    }

    void Update()
    {
    }


    public void ReduceWorkerTimer()
    {
        foreach (Worker worker in GameController.data.allWorkers.Values)
        {
            if (worker.population > 0)
            {
                switch (worker.status)
                {
                    case Status.WORKING:
                        TimeFunction.Countdown(ref worker.currentTime);
                        if (worker.currentTime <= 0)
                        {
                            worker.status = Status.EXHAUSTED;
                            worker.currentTime = worker.currentExhaustTime;
                        }
                        break;
                    case Status.RESTING:
                        if (worker.currentTime < worker.currentWorkTime)
                        {
                            TimeFunction.Countup(ref worker.currentTime, worker.currentWorkTime, 2 * worker.currentWorkTime / worker.currentExhaustTime);
                        }
                        break;
                    case Status.EXHAUSTED:
                        TimeFunction.Countdown(ref worker.currentTime);
                        if (worker.currentTime <= 0)
                        {
                            worker.status = Status.RESTING;
                            worker.currentTime = worker.currentWorkTime;
                        }
                        break;
                }
            }
        }
    }


    public void AdjustWorkerStats()
    {
        workerTPS = 0;
        foreach (Worker worker in GameController.data.allWorkers.Values)
        {
            if (worker.unlocked)
            {
                worker.populationCap = 250 * (1 + worker.isChaotic);
                worker.tpsMultiplier = 1 * (1 /*+ u1Max*/) * GameController.data.TPSGenericMultiplier * (1 + worker.isChaotic);
                if (!GameController.data.buyMax)
                {
                    worker.buyAmount = GameController.data.bulkBuy;
                    worker.costMultiplier = worker.buyAmount;
                } else
                {
                    long temp = (long) (GameController.data.time / worker.baseCost);
                    if (worker.population + temp > worker.populationCap)
                    {
                        temp = worker.populationCap - worker.population;
                    }
                    worker.buyAmount = temp;
                    worker.costMultiplier = worker.buyAmount;
                }
                worker.workTimeMultiplier = 1 * (1 + worker.isChaotic);
                worker.exhaustTimeMultiplier = 1 * (1 + worker.isChaotic);
                worker.currentCost = worker.baseCost * worker.costMultiplier;
                worker.currentTPS = worker.baseTPS * worker.tpsMultiplier;
                worker.currentWorkTime = (int) (worker.baseWorkTime * worker.workTimeMultiplier);
                worker.currentExhaustTime = (int) (worker.baseExhaustTime * worker.exhaustTimeMultiplier);

                switch (worker.status)
                {
                    case Status.WORKING:
                        worker.totalTPS = worker.currentTPS * worker.population;
                        break;
                    case Status.RESTING:
                        worker.totalTPS = 0;
                        break;
                    case Status.EXHAUSTED:
                        worker.totalTPS = 0;
                        break;
                }

                if (worker.population > 0)
                {
                    workerTPS += worker.totalTPS;
                }
            }
        }
        PushTPSToGameController(workerTPS);
    }

    void PushTPSToGameController(double workerTPS)
    {
        GameController.data.tickPerSecond = workerTPS;
    }
}
