using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoxSpawner : MonoBehaviour
{
    public GameObject healthBoxPrefab;
    public Transform healthBoxSpawner;
    public float respawnTime = 15f;

    private GameObject currentHealthBoxPrefab;
    private bool isRespawning = false;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAmmoBox();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealthBoxPrefab == null && !isRespawning)
        {
            StartCoroutine(RespawnAfterDelay());
        }
    }

    void SpawnAmmoBox()
    {
        currentHealthBoxPrefab = Instantiate(healthBoxPrefab, healthBoxSpawner.position, healthBoxSpawner.rotation);
    }

    IEnumerator RespawnAfterDelay()
    {
        isRespawning = true;
        yield return new WaitForSeconds(respawnTime);

        if (currentHealthBoxPrefab == null)
        {
            SpawnAmmoBox();
        }

        isRespawning = false;
    }
}
