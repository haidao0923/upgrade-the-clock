using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class archivesScript : MonoBehaviour
{
    public bool isRacePressed = false;
    Button btn;
    Image image; public Sprite unPressed, pressed;
    GameObject background, archives;
    long invoked1, count1;

    void Awake()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(onPointerDownRaceButton);

        image = GetComponent<Image>();
        background = GameObject.Find("Canvas/Menu/background/1/2");
        archives = background.transform.Find("Archives").gameObject; //change this and archives to name

    }

    void Start()
    {
    }

    void Update()
    {
        if (isRacePressed)  //change worker to button type
        {
            openMenu(); isRacePressed = false;
        }
        if (archives.activeSelf) //change worker to button type
        { image.sprite = pressed; }
        else { image.sprite = unPressed; }



        ////////////////////////////////////////////////////////////////requirements for new achivements (change height of secretArchives)
        for (int i = 0; i < GameController.data.pooledArchives.Count; i++)
        {
            Archives archive = GameController.data.pooledArchives[i];
            if (archive.unlocked == 1)
            {
                GameController.data.pooledArchives.Remove(archive);
                GameController.data.unlockedArchives.Add(archive);
            }
        }
        GameController.data.pooledArchives.TrimExcess();

        for (int i = 0; i < GameController.data.pooledArchives.Count; i++)
        {
            Archives archive = GameController.data.pooledArchives[i];
            if (archive.type == "atleastWorkers")
            {
                if (archive.worker.population >= archive.typeAmount)
                {
                    archive.unlocked = 1;
                    GameObject secretArchive = archives.transform.Find("secretArchives/" + archive.name).gameObject; secretArchive.transform.SetAsFirstSibling(); secretArchive.SetActive(true);
                    openMenu();
                }
            }
            else if (archive.type == "special")
            {
                if (BossManager.activeBosses.Count > 0 && invoked1 == 0)
                {
                    invoked1 = 1; count1 = GameController.data.tbossclicks; Invoke("PacifistMoreLikePacifier", 180);
                }
                if (count1 != GameController.data.tbossclicks) { CancelInvoke("PacifistMoreLikePacifier"); invoked1 = 0; }
            }
        }

        ////////////////////////////////////////////////////////////////show new achievements
        foreach (Archives archive in GameController.data.unlockedArchives)
        {
            GameObject secretArchive = archives.transform.Find("secretArchives/" + archive.name).gameObject; secretArchive.SetActive(true);
        }
    }

    public void onPointerDownRaceButton()
    {
        isRacePressed = true;
    }

    public void openMenu()
    {
        foreach (Transform child in background.transform)
        {
            child.gameObject.SetActive(false); archives.SetActive(true);
        }
    }

    public void PacifistMoreLikePacifier()
    {
        GameController.data.PacifistMoreLikePacifier.unlocked = 1; GameController.data.allWorkers[4].unlocked = true;
        GameObject secretArchive = archives.transform.Find("secretArchives/" + GameController.data.PacifistMoreLikePacifier.name).gameObject; secretArchive.transform.SetAsFirstSibling(); secretArchive.SetActive(true);
        openMenu();
    }
}