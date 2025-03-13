using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text highScoreUI;
    string newGameScene = "Game";

    public AudioClip bg_music;
    public AudioSource main_channel;

    private void Start()
    {
        main_channel.PlayOneShot(bg_music);

        //asignar la puntuacion mas alta
        int highScore = SaveLoadManager.THIS.LoadHighScore();
        highScoreUI.text = $"Top Wave Survived: {highScore}";
    }

    public void StartNewGame()
    {
        main_channel.Stop();

        SceneManager.LoadScene(newGameScene);
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
