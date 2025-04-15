using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeSpawner : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Transform grenadeSpawner;
    public float respawnTime = 15f;

    private GameObject currentGrenadePrefab;
    private bool isRespawning = false;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAmmoBox();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGrenadePrefab == null && !isRespawning)
        {
            StartCoroutine(RespawnAfterDelay());
        }
    }

    void SpawnAmmoBox()
    {
        currentGrenadePrefab = Instantiate(grenadePrefab, grenadeSpawner.position, grenadeSpawner.rotation);
    }

    IEnumerator RespawnAfterDelay()
    {
        isRespawning = true;
        yield return new WaitForSeconds(respawnTime);

        if (currentGrenadePrefab == null)
        {
            SpawnAmmoBox();
        }

        isRespawning = false;
    }
}
