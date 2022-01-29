using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class archivesTooltipScript : MonoBehaviour
{

    public GameObject secretArchives; Text secretArchivesText;
    int ownedArchives, totalArchives;

    void Start()
    {
        secretArchives = GameObject.Find("Canvas/Menu/background/1/2/Archives/secretArchives");
        secretArchivesText = GetComponent<Text>();
    }

    void Update()
    {
        checkOwnedArchives();
        totalArchives = secretArchives.transform.childCount - 1;
        secretArchivesText.text = "Secret Archives: " + ownedArchives + "/" + totalArchives;

    }

    void checkOwnedArchives()
    {
        ownedArchives = -1;
        foreach (Transform child in secretArchives.transform)
        {
            if (child.gameObject.activeSelf)
            {
                ownedArchives += 1;
            }
        }
    }
}
