using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager THIS { get; set; }

    public AudioSource shootingChannel;

    public AudioClip M1911Shot;
    public AudioClip M48Shot;

    public AudioSource reloadingSound1911;
    public AudioSource reloadingSoundM48;

    public AudioSource emptyMagazineSound;

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

    public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Pistol1911:
                shootingChannel.PlayOneShot(M1911Shot);
                break;
            case WeaponModel.M48:
                shootingChannel.PlayOneShot(M48Shot);
                break;
        }
    }

    public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Pistol1911:
                reloadingSound1911.Play();
                break;
            case WeaponModel.M48:
                reloadingSoundM48.Play();
                break;
        }
    }
}
