using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUILayout : MonoBehaviour
{
    public GameObject upgradePrefab;
    void Start()
    {
        foreach (KeyValuePair<int, Upgrade> upgrade in GameController.data.allUpgrades)
        {
            GameObject prefab = Instantiate(upgradePrefab, gameObject.transform);
            prefab.name = upgrade.Value.ToString();
            prefab.GetComponent<UpgradeUIPrefab>().upgrade = upgrade.Value;
            prefab.transform.SetSiblingIndex(upgrade.Key + 1);
        }
    }

    private void Update()
    {
        if (GameController.data.allUpgrades[0].unlocked == false)
        {
            GameController.data.allUpgrades[0].unlocked = true;
        }
        for (int i = 1; i < 4; i++)
        {
            if (GameController.data.allUpgrades[i].unlocked == false
                && GameController.data.allUpgrades[i - 1].level > 1)
            {
                GameController.data.allUpgrades[i].unlocked = true;
            }
        }

        if (GameController.data.unlockedUpgradeQueue.Count > 0)
        {
            transform.Find(GameController.data.unlockedUpgradeQueue.Dequeue().ToString()).gameObject.SetActive(true);
        }
    }
}
