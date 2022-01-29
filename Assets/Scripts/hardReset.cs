using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hardReset : MonoBehaviour {

    public GameObject oldGameController;
	
	public void onClick ()
    {
        DestroyImmediate(oldGameController.GetComponent<GameController>(), true);
        oldGameController.AddComponent<GameController>();
	}
}
