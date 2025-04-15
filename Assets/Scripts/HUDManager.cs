using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    public static HUDManager THIS { get; set; }

    [Header("Ammo")]
    public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;
    public Image ammoTypeUI;

    [Header("Weapon")]
    public Image activeWeaponUI;
    public Image unActiveWeaponUI;

    [Header("Throwables")]
    public Image lethalUI;
    public TextMeshProUGUI lethalAmmoUI;

    public Image tacticalUI;
    public TextMeshProUGUI tacticalAmmoUI;

    public Sprite emptySlot;
    public Sprite greySlot;

    public GameObject middleDot;

    public GameObject stopPanel;
    public GameObject helpPanel;

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

    private void Start()
    {
        stopPanel.SetActive(false);
        helpPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            stopPanel.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
            Cursor.visible = true; // Hace que el cursor sea visible
        }

        Weapon activeWeapon = WeaponManager.THIS.activeWeaponSlot.GetComponentInChildren<Weapon>();
        Weapon unActiveWeapon = GetUnActiveWeaponSlot().GetComponentInChildren<Weapon>();

        if (activeWeapon)
        {
            magazineAmmoUI.text = $"{activeWeapon.bulletsLeft / activeWeapon.bulletsPerBurst}";
            totalAmmoUI.text = $"{WeaponManager.THIS.CheckAmmoLeftFor(activeWeapon.thisWeaponModel)}";

            Weapon.WeaponModel model = activeWeapon.thisWeaponModel;
            ammoTypeUI.sprite = GetAmmoSprite(model);

            activeWeaponUI.sprite = GetWeaponSprite(model);

            if (unActiveWeapon)
            {
                unActiveWeaponUI.sprite = GetWeaponSprite(unActiveWeapon.thisWeaponModel);
            }
        }
        else
        {
            magazineAmmoUI.text = "";
            totalAmmoUI.text = "";

            ammoTypeUI.sprite = emptySlot;

            activeWeaponUI.sprite = emptySlot;
            unActiveWeaponUI.sprite = emptySlot;
        }

        if (WeaponManager.THIS.lethalsCount <= 0)
        {
            lethalUI.sprite = greySlot;
        }

        if (WeaponManager.THIS.tacticalCount <= 0)
        {
            tacticalUI.sprite = greySlot;
        }
    }

    private Sprite GetWeaponSprite(Weapon.WeaponModel model)
    {
        switch (model)
        {
            case Weapon.WeaponModel.Pistol1911:
                return Resources.Load<GameObject>("Pistol1911_Weapon").GetComponent<SpriteRenderer>().sprite;
            case Weapon.WeaponModel.M48:
                return Resources.Load<GameObject>("M48_Weapon").GetComponent<SpriteRenderer>().sprite;
            default:
                return null;
        }
    }

    private Sprite GetAmmoSprite(Weapon.WeaponModel model)
    {
        switch (model)
        {
            case Weapon.WeaponModel.Pistol1911:
                return Resources.Load<GameObject>("Pistol_Ammo").GetComponent<SpriteRenderer>().sprite;
            case Weapon.WeaponModel.M48:
                return Resources.Load<GameObject>("Rifle_Ammo").GetComponent<SpriteRenderer>().sprite;
            default:
                return null;
        }
    }

    private GameObject GetUnActiveWeaponSlot()
    {
        foreach (GameObject weaponSlot in WeaponManager.THIS.weaponSlots)
        {
            if(weaponSlot != WeaponManager.THIS.activeWeaponSlot)
            {
                return weaponSlot;
            }
        }
        //nunca va a pasar pero siempre se necesita devolver algo
        return null;
    }

    internal void UpdateThrowablesUI()
    {
        lethalAmmoUI.text = $"{WeaponManager.THIS.lethalsCount}";
        tacticalAmmoUI.text = $"{WeaponManager.THIS.tacticalCount}";

        switch (WeaponManager.THIS.equippedLethalType)
        {
            case Throwable.ThrowableType.Grenade:
                lethalUI.sprite = Resources.Load<GameObject>("Grenade").GetComponent<SpriteRenderer>().sprite;
                break;
        }

        switch (WeaponManager.THIS.equippedTacticalType)
        {
            case Throwable.ThrowableType.Smoke_Granade:
                tacticalUI.sprite = Resources.Load<GameObject>("Smoke_Granade").GetComponent<SpriteRenderer>().sprite;
                break;
        }
    }

    public void GameContinue()
    {
        stopPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
    }

    public void Exit()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void OpenHelpPanel()
    {
        helpPanel.SetActive(true);
        stopPanel.SetActive(false);
        Time.timeScale = 0f;

    }

    public void CloseHelpPanel()
    {
        helpPanel.SetActive(false);
        stopPanel.SetActive(true);
        Time.timeScale = 0f;

    }


}
