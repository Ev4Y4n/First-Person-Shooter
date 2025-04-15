using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField] float delay = 3f;
    [SerializeField] float damageRadius = 10f;
    [SerializeField] float explosionForce = 600f;

    float countdown;

    bool hasExploded = false;
    public bool hasBeenThrown = false;

    public enum ThrowableType
    {
        None,
        Grenade,
        Smoke_Granade
    }

    public ThrowableType throwableType;

    private void Start()
    {
        countdown = delay;
    }

    private void Update()
    {
        if (hasBeenThrown)
        {
            countdown -= Time.fixedDeltaTime;
            if(countdown<=0f && !hasExploded)
            {
                Explode();
                hasExploded = true;
            }
        }
    }

    private void Explode()
    {
        GetThrowableEffect();
        Destroy(gameObject);
    }

    private void GetThrowableEffect()
    {
        switch (throwableType)
        {
            case ThrowableType.Grenade:
                GrenadeEffect();
                break;
            case ThrowableType.Smoke_Granade:
                SmokeGrenadeEffect();
                break;
        }
    }

    private void SmokeGrenadeEffect()
    {
        //Efectos visuales
        GameObject smokeEffect = GlobalReferences.THIS.smokeGenerateEffect;
        Instantiate(smokeEffect, transform.position, transform.rotation);

        //Sonido granadas
        SoundManager.THIS.throwableChannel.PlayOneShot(SoundManager.THIS.grenadeSound);

        //Efectos físicos
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);
        foreach (Collider objectInRange in colliders)
        {
            Rigidbody rb = objectInRange.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //rb.AddExplosionForce(explosionForce, transform.position, damageRadius);
            }
            /*
            if (objectInRange.gameObject.GetComponent<Enemy>())
            {
                objectInRange.gameObject.GetComponent<Enemy>().TakeDamage(20);
            }
            */
        }
    }

    private void GrenadeEffect()
    {
        //Efectos visuales
        GameObject explosionEffect = GlobalReferences.THIS.grenadeExplosionEffect;
        Instantiate(explosionEffect, transform.position, transform.rotation);

        //Sonido granadas
        SoundManager.THIS.throwableChannel.PlayOneShot(SoundManager.THIS.grenadeSound);

        //Efectos físicos
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);
        foreach(Collider objectInRange in colliders)
        {
            Rigidbody rb = objectInRange.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, damageRadius);
            }

            if (objectInRange.gameObject.GetComponent<Enemy>())
            {
                objectInRange.gameObject.GetComponent<Enemy>().TakeDamage(100);
            }
        }
    }
}
