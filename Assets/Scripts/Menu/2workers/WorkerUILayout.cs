using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerUILayout : MonoBehaviour
{
    public GameObject workerPrefab;
    void Start()
    {
        foreach (KeyValuePair<int, Worker> worker in GameController.data.allWorkers)
        {
            GameObject prefab = Instantiate(workerPrefab, gameObject.transform);
            prefab.name = worker.Value.ToString();
            prefab.GetComponent<WorkerUIPrefab>().worker = worker.Value;
            prefab.transform.SetSiblingIndex(worker.Key + 1);
        }
    }

    private void Update()
    {
        if (GameController.data.allWorkers[0].unlocked == false)
        {
            GameController.data.allWorkers[0].unlocked = true;
        }
        for (int i = 1; i < 4; i++)
        {
            if (GameController.data.allWorkers[i].unlocked == false
                && GameController.data.allWorkers[i - 1].population > 0)
            {
                GameController.data.allWorkers[i].unlocked = true;
            }
        }

        if (GameController.data.unlockedWorkerQueue.Count > 0)
        {
            transform.Find(GameController.data.unlockedWorkerQueue.Dequeue().ToString()).gameObject.SetActive(true);
        }
    }
}
