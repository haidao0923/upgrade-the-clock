using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TimeFunction;

public class WorkerDatabase : MonoBehaviour
{
    public Sprite[] workerSprites;

    void Awake()
    {
        InitializeData();
    }

    public void InitializeData()
    {
        GameController.data.allWorkers = new Dictionary<int, Worker> { // Name, Cost, Damage, Work Time, Exhaust Time, Quote
            { 0, new Worker(0, "StarGazer", workerSprites[0],
                            10 * TimeUnit.minute,
                            1 * TimeUnit.second,
                            10 * (int)TimeUnit.minute,
                            1 * (int)TimeUnit.minute,
                            "Looking for a job with a VERY flexible schedule. You never know when a new star is born.") },
            { 1, new Worker(1, "KoolKid", workerSprites[1],
                            2 * TimeUnit.hour,
                            3 * TimeUnit.second,
                            1 * (int)TimeUnit.hour,
                            5 * (int)TimeUnit.minute,
                            "It's not child cruelty if you give them 5 minute breaks and sunglasses, right?") },
            { 2, new Worker(2, "Teenager", workerSprites[2],
                            4 * TimeUnit.day,
                            16 * TimeUnit.second,
                            4 * (int)TimeUnit.hour,
                            1 * (int)TimeUnit.hour,
                            "#Part-y-Time4ever #2Cool4Work") },
            { 3, new Worker(3, "Shepherd", workerSprites[3],
                            90 * TimeUnit.day,
                            333 * TimeUnit.second,
                            10 * (int)TimeUnit.hour + 30 * (int)TimeUnit.minute,
                            4 * (int)TimeUnit.hour + 10 * (int)TimeUnit.minute,
                            "From sunrise to sunset, that is how long we work until we give ourselves a nice, long rest.")},
            { 4, new Worker(4, "Snowman", workerSprites[4],
                            90 * TimeUnit.day,
                            2 * TimeUnit.minute + 30 * TimeUnit.second,
                            6 * (int)TimeUnit.hour,
                            2 * (int)TimeUnit.hour + 30 * (int)TimeUnit.minute,
                            "A good snowman take a long time to melt, but also a long time to rebuild.")},
        };
    }
}