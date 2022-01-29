using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animatedPengelsScript : MonoBehaviour {

    int health;
    Image img;

    void Awake()
    {
        img = GetComponent<Image>();
    }

    void OnEnable()
    {
        health = 3;
    }

    void Update()
    {
        switch(health)
        {
            case 3:
                img.color = new Color(1, 1, 1, 1);
                break;
            case 2:
                img.color = new Color(1, 0.5f, 0.5f, 1);
                break;
            case 1:
                img.color = new Color(1, 0, 0, 1);
                break;
            case 0:
                if (GameController.data.pengelCount >= 2)
                {
                    GameController.data.pengelCount = 1;
                    GameController.data.timeUntilNextPengel = UnityEngine.Random.Range(1, 600);
                }
                else
                {
                    GameController.data.pengelCount = 0;
                    GameController.data.timeUntilNextPengel = UnityEngine.Random.Range(1, 600);
                }
                GameController.data.timeCoins += 1;
                gameObject.SetActive(false);
                break;
        }

    }

	public void onClick()
    {
        health -= 1;
    }
}
