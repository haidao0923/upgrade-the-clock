using UnityEngine;
using UnityEngine.UI;

public class WorkerUIPrefab : MonoBehaviour
{

    Button hireButton, restButton; Image restButtonIMG;
    Text scriptedName, quote; Image image;
    Text population, hireText, cost, tps, totalTPS, workTime, exhaustTime, currentTime, restText;   //GET MENU ITEM

    public Worker worker;

    void Start()
    {
        scriptedName = transform.Find("Name").GetComponent<Text>();
        quote = transform.Find("Quote").GetComponent<Text>();
        image = transform.Find("Images/Image").GetComponent<Image>();

        hireButton = transform.Find("Hire").GetComponent<Button>();
        hireButton.onClick.AddListener(onHireButtonPressed);
        hireText = transform.Find("Hire/Text").GetComponent<Text>();
        cost = transform.Find("Hire/Cost").GetComponent<Text>();

        restButton = transform.Find("Rest").GetComponent<Button>();
        restButton.onClick.AddListener(onRestButtonPressed);
        restButtonIMG = transform.Find("Rest").GetComponent<Image>();
        restText = transform.Find("Rest/Text").GetComponent<Text>();

        population = transform.Find("Population").GetComponent<Text>();
        tps = transform.Find("TPS").GetComponent<Text>();
        totalTPS = transform.Find("TotalTPS").GetComponent<Text>();

        currentTime = transform.Find("Current Time").GetComponent<Text>();
        workTime = transform.Find("Work Time").GetComponent<Text>();
        exhaustTime = transform.Find("Exhaust Time").GetComponent<Text>();
    }

    private void OnEnable()
    {
        scriptedName.text = worker.name;
        quote.text = worker.quote;
        image.sprite = worker.image;
    }

    void Update()
    {
        worker.tpsDisplay = TimeFunction.ConvertValueToString(worker.currentTPS);
        worker.totalTPSDisplay = TimeFunction.ConvertValueToString(worker.totalTPS);
        worker.costDisplay = TimeFunction.ConvertValueToString(worker.currentCost);

        worker.currentTimeDisplay = TimeFunction.ConvertValueToTimerDisplay(worker.currentTime);
        worker.workTimeDisplay = TimeFunction.ConvertValueToTimerDisplay(worker.currentWorkTime);
        worker.exhaustTimeDisplay = TimeFunction.ConvertValueToTimerDisplay(worker.currentExhaustTime);

        //////////////////////////////////////////////////////////////////////////////// CHECK IF CAN PURCHASE
        if (worker.buyAmount > 0 && GameController.data.canPurchase(worker.currentCost)
            && (worker.population + worker.buyAmount <= worker.populationCap))
        {
            hireButton.interactable = true;
        }
        else { hireButton.interactable = false; }

        //////////////////////////////////////////////////////////////////////////////// CHECK IF CAN REST
        if (worker.status != Status.EXHAUSTED && worker.population > 0)
        {
            restButton.interactable = true;
        }
        else { restButton.interactable = false; }

        //////////////////////////////////////////////////////////////////////////////// WORKER REST TEXT AND COLOR
        if (worker.population > 0)
        {
            switch (worker.status)
            {
                case Status.WORKING:
                    currentTime.color = Color.white;
                    restButtonIMG.color = new Color(1, 0.5058824f, 0); restText.text = "REST";
                    break;
                case Status.RESTING:
                    currentTime.color = Color.green;
                    restButtonIMG.color = Color.blue; restText.text = "WORK";
                    break;
                case Status.EXHAUSTED:
                    currentTime.color = Color.red;
                    restButtonIMG.color = Color.red; restText.text = "IS SICK";
                    break;
            }
        }
        else { restButtonIMG.color = new Color(0, 0.1781249f, 1); restText.text = "WORK"; }

        ////////////////////////////////////////////////////////////////////////////////
        population.text = worker.population.ToString() + " / " + worker.populationCap.ToString();
        tps.text = worker.tpsDisplay;
        totalTPS.text = worker.totalTPSDisplay;
        if (worker.buyAmount > 0)
        {
            hireText.text = "HIRE " + worker.buyAmount;
        } else
        {
            hireText.text = "HIRE";
        }
        cost.text = worker.costDisplay;
        currentTime.text = worker.currentTimeDisplay;
        workTime.text = worker.workTimeDisplay;
        exhaustTime.text = worker.exhaustTimeDisplay;

    }

    private void onHireButtonPressed()
    {
        //pay
        GameController.data.purchaseCalculator(worker.currentCost);
        //start current time timer
        if (worker.population == 0)
        {
            worker.currentTime = worker.currentWorkTime;
            worker.status = Status.WORKING;
        }
        //population increase
        worker.population += worker.buyAmount;
    }

    private void onRestButtonPressed()
    {
        switch (worker.status)
        {
            case Status.WORKING:
                worker.status = Status.RESTING;
                break;
            case Status.RESTING:
                worker.status = Status.WORKING;
                break;
        }
        GameController.data.Save();
    }
}
