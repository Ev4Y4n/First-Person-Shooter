using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public TMP_Text highScoreUI;
    string newGameScene = "Game";

    public AudioClip bg_music;
    public AudioSource main_channel;

    public Image musicOn;
    public Image musicOff;

    public bool musicActive =true;

    private void Start()
    {
        main_channel.PlayOneShot(bg_music);

        //asignar la puntuacion mas alta
        int highScore = SaveLoadManager.THIS.LoadHighScore();
        highScoreUI.text = $"Top Wave Survived: {highScore}";

        musicOn.gameObject.SetActive(true);
        musicOff.gameObject.SetActive(false);
    }

    public void StartNewGame()
    {
        main_channel.Stop();

        SceneManager.LoadScene(newGameScene);
    }

    public void MusicOn()
    {
        musicActive = !musicActive;
        if (musicActive)
        {
            main_channel.Stop();
            musicOn.gameObject.SetActive(false);
            musicOff.gameObject.SetActive(true);
            
        }
        else
        {
            main_channel.PlayOneShot(bg_music);
            musicOn.gameObject.SetActive(true);
            musicOff.gameObject.SetActive(false);
        }
    }

    

    public void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
