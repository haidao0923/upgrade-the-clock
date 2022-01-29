using UnityEngine;
using UnityEngine.UI;

public class PersistentUI : MonoBehaviour
{

    public Text bigText, smallText, offlineGainsText;
    public GameObject offlineGains;

    void Start()
    {
        bigText = GameObject.Find("Canvas/Big Text").GetComponent<Text>();
        smallText = GameObject.Find("Canvas/Small Text").GetComponent<Text>();
        offlineGains = GameObject.Find("Canvas/Offline Gains Display/scriptedText");
        offlineGainsText = offlineGains.GetComponent<Text>();

    }

    void Update()
    {
        if (offlineGains.activeSelf)
        {
            offlineGainsText.text = TimeFunction.ConvertValueToString(GameController.data.offlineGain);
        }
        if (GameController.data.life > 99 || GameController.data.dimension > 99 || TimeFunction.GetEpoch(GameController.data.time) > 9999)
        {
            bigText.text = "Dime. " + GameController.data.dimension + " Life " + GameController.data.life + " Epoch " + TimeFunction.GetEpoch(GameController.data.time);
        }
        else { bigText.text = "Dimension " + GameController.data.dimension + " Life " + GameController.data.life + " Epoch " + TimeFunction.GetEpoch(GameController.data.time); }
        smallText.text = TimeFunction.GetYear(GameController.data.time) + " years " + TimeFunction.GetDay(GameController.data.time) + " days " 
        + TimeFunction.GetHour(GameController.data.time) + " hours " + TimeFunction.GetMinute(GameController.data.time) + " minutes " + TimeFunction.GetSecond(GameController.data.time) + " seconds";
    }
}
