using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager THIS { get; set; }

    public string highScoreKey = "BestWaveSavedValue";

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

        DontDestroyOnLoad(this);
    }


    public void SaveHighScore(int score)
    {
        PlayerPrefs.SetInt(highScoreKey, score);
    }

    public int LoadHighScore()
    {
        if (PlayerPrefs.HasKey(highScoreKey))
        {
            return PlayerPrefs.GetInt(highScoreKey);
        }
        else
        {
            return 0;
        }

    }
}
