using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxSpawner : MonoBehaviour
{

    public GameObject ammoBoxPrefab; 
    public Transform ammoBoxSpawner; 
    public float respawnTime = 15f; 

    private GameObject currentAmmoBoxPrefab; 
    private bool isRespawning = false; 

    // Start is called before the first frame update
    void Start()
    {
        SpawnAmmoBox(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAmmoBoxPrefab == null && !isRespawning) 
        {
            StartCoroutine(RespawnAfterDelay()); 
        }
    }

    void SpawnAmmoBox()
    {
        currentAmmoBoxPrefab = Instantiate(ammoBoxPrefab, ammoBoxSpawner.position, ammoBoxSpawner.rotation); 
    }

    IEnumerator RespawnAfterDelay()
    {
        isRespawning = true; 
        yield return new WaitForSeconds(respawnTime); 

        if (currentAmmoBoxPrefab == null) 
        {
            SpawnAmmoBox(); 
        }

        isRespawning = false; 
    }
}
