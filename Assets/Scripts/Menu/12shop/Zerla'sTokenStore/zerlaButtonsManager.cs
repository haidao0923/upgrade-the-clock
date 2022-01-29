using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zerlaButtonsManager : MonoBehaviour {

    GameObject toolTips, shop;

    void Start()
    {
        toolTips = transform.Find("toolTips").gameObject;
        shop = transform.Find("Shop").gameObject;
    }

	public void Shop()
    {
        toolTips.SetActive(false); shop.SetActive(true);
    }

    public void Talk()
    {

    }

    public void Beg()
    {

    }

    public void Fight()
    {

    }
}
