using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalReferences : MonoBehaviour
{
    public static GlobalReferences THIS { get; set; }

    public GameObject bulletImpactEffectPrefab;

    private void Awake()
    {
        if(THIS !=null && THIS !=this)
        {
            Destroy(gameObject);
        }
        else
        {
            THIS = this;
        }
    }


}
