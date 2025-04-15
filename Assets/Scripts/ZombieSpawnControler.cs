using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZombieSpawnControler : MonoBehaviour
{
    public int initialZombiePerWave = 1;
    public int currentZombiePerWave;

    public float spawnDelay = 0.5f; //tiempo de espera de aparicion decada zombie en una misma oleada

    public int currentWave = 0;  
    public float waveCooldown=10.0f; //segudnos entre las oleadas

    public bool inCooldown;
    public float cooldownCounter=0; //para pruebas y UI

    public List<Enemy> currentZombiesAlive;

    public GameObject zombiePrefab;

    public TextMeshProUGUI waveOverUI;
    public TextMeshProUGUI cooldownCounterUI;
    public TextMeshProUGUI currentWaveUI;


    private void Start()
    {
        currentZombiePerWave = initialZombiePerWave;
        GlobalReferences.THIS.waveNumber = currentWave;
        StartNextWave();
    }

    private void StartNextWave()
    {
        currentZombiesAlive.Clear();
        currentWave++;
        GlobalReferences.THIS.waveNumber = currentWave;
        currentWaveUI.text = "Wave: " + currentWave.ToString();
        StartCoroutine(SpawnWave());
    }

    private void Update()
    {
        //get all dead zombies
        List<Enemy> zombiesToRemove = new List<Enemy>();
        foreach(Enemy zombie in currentZombiesAlive)
        {
            if (zombie.isDead)
            {
                zombiesToRemove.Add(zombie);
            }
        }

        //actually remove all dead zombies
        foreach (Enemy zombie in zombiesToRemove)
        {
            currentZombiesAlive.Remove(zombie);
        }

        zombiesToRemove.Clear();

        //start cooldown if all zombies are dead
        if(currentZombiesAlive.Count==0 && !inCooldown)
        {
            StartCoroutine(WaveCooldown());
        }
        
        //run the cooldown counter
        if (inCooldown)
        {
            cooldownCounter -= Time.deltaTime;
        }
        else
        {
            cooldownCounter = waveCooldown;
        }
        

        cooldownCounterUI.text = cooldownCounter.ToString("F0");//F0 para quitar los decimales
    }

    private IEnumerator WaveCooldown()
    {
        inCooldown = true;
        waveOverUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(waveCooldown);

        inCooldown = false;
        waveOverUI.gameObject.SetActive(false);
        currentZombiePerWave *= 2; //5*2=10 10*2=20... //esto hace que en cada oleada haya mas y mas zombies
        StartNextWave();
    }

    private IEnumerator SpawnWave()
    {
        for(int i=0; i < currentZombiePerWave; i++)
        {
            //generate a random offest within a specified range
            Vector3 spawnOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            Vector3 spawnPosition = transform.position + spawnOffset;

            //instanciar zombies
            var zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);

            //acceder al script del enemigo
            Enemy enemyScript = zombie.GetComponent<Enemy>();

            //rastrear a este zombie 
            currentZombiesAlive.Add(enemyScript);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
