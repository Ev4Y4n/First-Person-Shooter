using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerBottle : MonoBehaviour
{
    public List<Rigidbody> allParts = new List<Rigidbody>();

    public void Shatter()
    {
        foreach(Rigidbody part in allParts)
        {
            part.isKinematic = false; //al cambiar a no quinematico, las partes de la botella saldrán volando creando el efecto de explosión

        }
    }
}
