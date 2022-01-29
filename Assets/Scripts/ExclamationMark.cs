using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExclamationMark : MonoBehaviour
{
    void Start()
    {
        transform.parent.GetComponent<Button>().onClick.AddListener(DestroyExclamationMark);
    }

    private void DestroyExclamationMark()
    {
        Destroy(gameObject);
    }

}
