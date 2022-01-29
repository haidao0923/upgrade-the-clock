using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class awakenRecollectionsDisplay : MonoBehaviour
{

    void Start()
    {
        foreach (Recollections recollection in GameController.data.pooledRecollections)
        {
            transform.Find("recollections/" + recollection.name).gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        Canvas.ForceUpdateCanvases();
    }

    void Update()
    {
        foreach (Recollections recollection in GameController.data.unlockedRecollections)
        {
            transform.Find("recollections/" + recollection.name).gameObject.SetActive(true);
            GetComponent<VerticalLayoutGroup>().spacing += 1; // **
            GetComponent<VerticalLayoutGroup>().spacing -= 1; // **

        }

        foreach (Recollections recollection in GameController.data.pooledRecollections)
        {
            if (GameController.data.rebirthCount >= recollection.tier && !GameController.data.unlockedRecollections.Contains(recollection))
            {
                GameController.data.unlockedRecollections.Add(recollection);
            }
        }
    }
}