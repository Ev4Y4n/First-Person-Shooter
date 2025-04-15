using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : MonoBehaviour
{
    private int lifeAmount = 30;

    public void CollectBox(Player player)
    {
        player.TakeHealthBox(lifeAmount);  
    }
}
