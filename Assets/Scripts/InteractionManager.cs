using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager THIS { get; set; }

    public Weapon hoveredWeapon=null;
    public AmmoBox hoveredAmmoBox = null;
    public Throwable hoveredThrowable = null;
    public HealthBox hoveredHealthBox = null;

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
                //Desactivar el outline del arma seleccionada anteriormente
                if (hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }
                
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
                if (hoveredAmmoBox)
                {
                    hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                }

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
            if (objectHitByRaycast.GetComponent<HealthBox>())
            {
                if (hoveredHealthBox)
                {
                    hoveredHealthBox.GetComponent<Outline>().enabled = false;
                }
                hoveredHealthBox = objectHitByRaycast.gameObject.GetComponent<HealthBox>();
                hoveredHealthBox.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    Player player = FindObjectOfType<Player>();  // Encuentra el jugador en la escena
                    if (player != null)
                    {
                        hoveredHealthBox.CollectBox(player);  // Llama al método para sumar la vida
                    }

                    Destroy(objectHitByRaycast.gameObject); //destruir la caja de balas después de recogerlo
                }
            }
            else
            {
                if (hoveredHealthBox)
                {
                    hoveredHealthBox.GetComponent<Outline>().enabled = false;
                }

            }

            //Throwables
            if (objectHitByRaycast.GetComponent<Throwable>())
            {
                if (hoveredThrowable)
                {
                    hoveredThrowable.GetComponent<Outline>().enabled = false;
                }

                hoveredThrowable = objectHitByRaycast.gameObject.GetComponent<Throwable>();
                hoveredThrowable.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.THIS.PickupThrowable(hoveredThrowable);
                }
            }
            else
            {
                if (hoveredThrowable)
                {
                    hoveredThrowable.GetComponent<Outline>().enabled = false;
                }

            }

        }
    }
}
