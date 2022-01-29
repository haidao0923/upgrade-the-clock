using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeShopBossTokens : MonoBehaviour
{

    public Transform optionA, optionB, more;
    public List<GameObject> items = new List<GameObject>();

    void Start()
    {
        optionA = transform.Find("OptionA");
        optionB = transform.Find("OptionB");
        more = transform.Find("More");
        items.Add(optionA.GetChild(0).gameObject); items.Add(optionA.GetChild(1).gameObject); items.Add(optionA.GetChild(2).gameObject);
        items.Add(optionB.GetChild(0).gameObject); items.Add(optionB.GetChild(1).gameObject); items.Add(optionB.GetChild(2).gameObject);
    }

    public void OnEnable()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Confirmation");
        foreach (GameObject confirmation in temp)
        {
            confirmation.SetActive(false);
        }
    }

    void Update()
    {
        GetComponent<VerticalLayoutGroup>().spacing += 1; // **
        GetComponent<VerticalLayoutGroup>().spacing -= 1; // **

        unlockNewTiers();

        for (int i = 0; i < items.Count; i++)
        {
            if (GameController.data.timeCoins >= GameController.data.TimeShopBossTokensCost[i]) //change unit "check if can afford"
            {
                items[i].transform.Find("Buy").GetComponent<Button>().interactable = true;
            }
            else
            {
                items[i].transform.Find("Buy").GetComponent<Button>().interactable = false;
            }
            items[i].transform.Find("Buy/Cost").GetComponent<Text>().text = GameController.data.TimeShopBossTokensCost[i].ToString(); //change unit
        }
    }

    public void Purchase(GameObject go)
    {
        int temp = items.IndexOf(go); Debug.Log(go);
        GameController.data.timeCoins -= GameController.data.TimeShopBossTokensCost[temp];      //change unit
        GameController.data.bossTokens += GameController.data.TimeShopBossTokensReward[temp];   //change unit
    }

    void unlockNewTiers()
    {
        if (GameController.data.bossKilled >= 40 && items[0].activeSelf == false)                                   //Unlock new tiers
        { items[0].SetActive(true); }
        if (GameController.data.bossKilled >= 60 && items[1].activeSelf == false)
        { items[1].SetActive(true); }
        else if (GameController.data.bossKilled < 60) { items[1].SetActive(false); }
        if (GameController.data.bossKilled >= 80 && items[2].activeSelf == false)
        { items[2].SetActive(true); }
        else if (GameController.data.bossKilled < 80) { items[2].SetActive(false); }
        if (GameController.data.bossKilled >= 110 && items[3].activeSelf == false)
        { items[3].SetActive(true); }
        else if (GameController.data.bossKilled < 110) { items[3].SetActive(false); }
        if (GameController.data.bossKilled >= 150 && items[4].activeSelf == false)
        { items[4].SetActive(true); }
        else if (GameController.data.bossKilled < 150) { items[4].SetActive(false); }
        if (GameController.data.bossKilled >= 200 && items[5].activeSelf == false)
        { items[5].SetActive(true); }
        else if (GameController.data.bossKilled < 200) { items[5].SetActive(false); }
        if (GameController.data.bossKilled >= 110)                                                                  //Add "more" button
        {
            more.gameObject.SetActive(true);
        }
        else { optionB.gameObject.SetActive(false); more.gameObject.SetActive(false); }
    }
}
