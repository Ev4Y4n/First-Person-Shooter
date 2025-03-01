using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public bool isActiveWeapon;

    //Disparos
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay=2f;

    //Cargador arma
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    //Expandir
    public float spreadInsensity;

    //Bala
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f;

    public GameObject muzzleEffect;
    internal Animator animator; //se puede acceder en otros scripts pero no en el inspetor

    //Recarga
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;

    public Vector3 spawnPosition;
    public Vector3 spawnRotation;

    public enum WeaponModel
    {
        Pistol1911,
        M48
    }

    public WeaponModel thisWeaponModel;

    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    
    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        animator=GetComponent<Animator>();

        bulletsLeft = magazineSize;
    }

    private void Start()
    {

        if (muzzleEffect == null)
        {
            muzzleEffect = GameObject.Find("muzzelFlash 01");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActiveWeapon)
        {
            GetComponent<Outline>().enabled = false;

            if (bulletsLeft == 0 && isShooting)
            {
                SoundManager.THIS.emptyMagazineSound.Play();
            }

            if (currentShootingMode == ShootingMode.Auto)
            {
                //Mantener pulsado el boton derecho
                isShooting = Input.GetKey(KeyCode.Mouse0);
            }
            else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
            {
                //Presionar una vez boton derecho
                isShooting = Input.GetKeyDown(KeyCode.Mouse0);
            }

            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && isReloading == false)
            {
                Reload();
            }

            //Para una recarga automática
            if (readyToShoot && !isShooting && isReloading == false && bulletsLeft <= 0)
            {
                Reload();
            }

            if (readyToShoot && isShooting && bulletsLeft > 0)
            {
                burstBulletsLeft = bulletsPerBurst;
                FireWeapon();
            }

            if (AmmoManager.THIS.ammoDisplay != null)
            {
                AmmoManager.THIS.ammoDisplay.text = $"{bulletsLeft / bulletsPerBurst}/{magazineSize / bulletsPerBurst}";

            }
        }
        
    }

    private void FireWeapon()
    {
        bulletsLeft--;

        //StartCoroutine(MuzzleEffect());
        animator.SetTrigger("Shoot");

        SoundManager.THIS.PlayShootingSound(thisWeaponModel);

        readyToShoot = false;

        Vector3 shootingDirection=CalculateDirefctionAndSpread().normalized;

        //Instanciar balla
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        //Hacer que la bala mire hacia la direccion de disparo
        bullet.transform.forward = shootingDirection;

        //Disparar bala
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);
        //Destruir bala
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        //Comprobar si hemos terminado de disparar
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }
    
        //Modo cargador
        if(currentShootingMode==ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }

    private void Reload()
    {
        SoundManager.THIS.PlayReloadSound(thisWeaponModel);

        animator.SetTrigger("Reload");

        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        bulletsLeft = magazineSize;
        isReloading = false;
    }

    IEnumerator MuzzleEffect()
    {
        Debug.Log("Muzzle ture");
        muzzleEffect.GetComponent<Light>().enabled = true;
        muzzleEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        muzzleEffect.GetComponent<Light>().enabled = false;
        muzzleEffect.SetActive(false);
        Debug.Log("Muzzle false");

    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    private Vector3 CalculateDirefctionAndSpread()
    {
        //Disparar desde el centro de la pantalla para comprobar hacia donde se esta apuntando 
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray,out hit))
        {
            //Disparar a algo
            targetPoint = hit.point;
        }
        else
        {
            //Disparar al aire
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float x = UnityEngine.Random.Range(-spreadInsensity, spreadInsensity);
        float y = UnityEngine.Random.Range(-spreadInsensity, spreadInsensity);

        //Devuelve la direccion y el spread de disparo
        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

}
