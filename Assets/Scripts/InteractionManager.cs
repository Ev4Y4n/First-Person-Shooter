using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager THIS { get; set; }

    public Weapon hoveredWeapon=null;
    public AmmoBox hoveredAmmoBox = null;

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

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;

            //Armas
            if (objectHitByRaycast.GetComponent<Weapon>() && objectHitByRaycast.GetComponent<Weapon>().isActiveWeapon==false)
            {
                hoveredWeapon = objectHitByRaycast.gameObject.GetComponent<Weapon>();
                hoveredWeapon.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.THIS.PickupWeapon(objectHitByRaycast);

            }   }
            else
            {
                if (hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }
                
            }
            //Caja de balas
            if (objectHitByRaycast.GetComponent<AmmoBox>())
            {
                hoveredAmmoBox = objectHitByRaycast.gameObject.GetComponent<AmmoBox>();
                hoveredAmmoBox.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.THIS.PickupAmmo(hoveredAmmoBox);
                    Destroy(objectHitByRaycast.gameObject); //destruir la caja de balas después de recogerlo
                }
            }
            else
            {
                if (hoveredAmmoBox)
                {
                    hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                }

            }

        }
    }
}
