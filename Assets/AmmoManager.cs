using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public static AmmoManager THIS { get; set; }

    //UI
    public TextMeshProUGUI ammoDisplay;

    private void Awake()
    {
        if (THIS != null && THIS != this)
        {
            Destroy(gameObject);
        }
        else
        {
            THIS = this;
        }
    }
}

