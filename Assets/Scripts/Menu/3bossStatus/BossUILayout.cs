using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUILayout : MonoBehaviour
{
    public GameObject prefab;
    private BossManager bossManager; private Transform bossButtonParent;

    private void Start()
    {
        bossManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BossManager>();
        bossButtonParent = GameObject.Find("Canvas/Bosses").transform;
    }

    public void CreatePrefab(Boss boss)
    {
        GameObject bossInfoDisplay = Instantiate(prefab, gameObject.transform);
        bossInfoDisplay.name = boss.id.ToString();
        bossInfoDisplay.transform.SetSiblingIndex(1);
        BossUIPrefab tempScript = bossInfoDisplay.GetComponent<BossUIPrefab>();
        tempScript.boss = boss;
        CreateBossButton(boss, bossInfoDisplay);
    }

    public void CreateBossButton(Boss boss, GameObject bossInfoDisplay)
    {
        GameObject temp = Instantiate(bossManager.bossButtons[boss.id][boss.level], bossButtonParent);
        temp.transform.SetAsFirstSibling();
        temp.GetComponent<BossUIButton>().boss = boss;
        temp.GetComponent<BossUIButton>().bossInfoDisplay = bossInfoDisplay;
    }
}
