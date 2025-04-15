using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public int HP = 180;
    public GameObject bloodyScreen;

    public TextMeshProUGUI playerHealthUI;
    public GameObject gameOverUI;

    public bool isDead;

    private Animator animator;


    private void Start()
    {
        playerHealthUI.text = $"Health: {HP}";
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)//si atacas al zombie y aun no tiene la vida en 0
        {
            print("Muerte del jugador");
            PlayerDead();
            isDead = true;

        }
        else //realiza la animación de daño
        {
            print("Daño al jugador");
            StartCoroutine(bloodyScreenEffect());
            playerHealthUI.text = $"Health: {HP}";
            SoundManager.THIS.playerChannel.PlayOneShot(SoundManager.THIS.playerHurt);

        }
    }

    public void TakeHealthBox(int lifeAmount)
    {
        HP += lifeAmount;  
        if (HP > 180)  
        {
            HP = 180;
        }

        playerHealthUI.text = $"Health: {HP}";
    }

    private void PlayerDead()
    {
        SoundManager.THIS.playerChannel.PlayOneShot(SoundManager.THIS.playerDie);
        
        SoundManager.THIS.playerChannel.clip= SoundManager.THIS.gameOverMusic;
        SoundManager.THIS.playerChannel.PlayDelayed(2f);
        
        GetComponent<MouseMovement>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;

        //Dying animation
        GetComponentInChildren<Animator>().enabled = true;
        playerHealthUI.gameObject.SetActive(false);

        GetComponent<ScreenFader>().StartFade();
        StartCoroutine(ShowGameOverUI());
    }

    private IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(1f);
        gameOverUI.gameObject.SetActive(true);

        int waveSurvived = GlobalReferences.THIS.waveNumber;

        if (waveSurvived - 1 > SaveLoadManager.THIS.LoadHighScore())
        {

            SaveLoadManager.THIS.SaveHighScore(waveSurvived-1);//par conseguir la ultima oleada 
        }

        StartCoroutine(ReturnToMainMenu());
    }

    private IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
        
    }

    private IEnumerator bloodyScreenEffect()
    {
        if (bloodyScreen.activeInHierarchy == false)
        {
            bloodyScreen.SetActive(true);
        }

        var image = bloodyScreen.GetComponentInChildren<Image>();

        // Set the initial alpha value to 1 (fully visible).
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the new alpha value using Lerp.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // Update the color with the new alpha value.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;

            yield return null; ; // Wait for the next frame.
        }


        if (bloodyScreen.activeInHierarchy == true)
        {
            bloodyScreen.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("ZombieHand"))
        {
            if (isDead == false)
            {
                TakeDamage(collision.gameObject.GetComponent<ZombieHand>().damage);
            }
        }
        
    }
}
