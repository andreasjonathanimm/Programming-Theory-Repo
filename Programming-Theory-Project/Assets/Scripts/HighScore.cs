using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Saves the Highscore
/// </summary>
public class HighScore : MonoBehaviour
{
    // Encapsulation
    private float score;
    private PlayerController playerController;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        // Make a new highscore if it isn't saved the first time
        if(!PlayerPrefs.HasKey("highscore"))
        {
            TheHighScore tempHighscore = new TheHighScore() { score = 0 };
            string json = JsonUtility.ToJson(tempHighscore);
            PlayerPrefs.SetString("highscore", json);
            PlayerPrefs.Save();
        }

        // Load the highscore
        string jsonString = PlayerPrefs.GetString("highscore");
        TheHighScore highscore = JsonUtility.FromJson<TheHighScore>(jsonString);
        score = highscore.score;

        // Update the best score text
        bestScoreText.text = "Best Score: " + score;
    }

    private void Update()
    {
        // If the Player scored more than the best score, starts increasing the best score;
        // Once it's game over, save the new best score
        if(playerController.score > score)
        {
            score = playerController.score;
            bestScoreText.text = "Best Score: " + score;
            if (playerController.gameOver)
            {
                SaveBestScore(score);
            }
        }
    }

    // Abstraction
    private void SaveBestScore(float score)
    {
        // Declare a new highscore
        TheHighScore tempHighscore = new TheHighScore() { score = score };

        string jsonString = PlayerPrefs.GetString("highscore");
        TheHighScore highscore = JsonUtility.FromJson<TheHighScore>(jsonString);

        highscore = tempHighscore;

        string json = JsonUtility.ToJson(highscore);
        PlayerPrefs.SetString("highscore", json);
        PlayerPrefs.Save();

    }

    // Abstraction & Encapsulation
    [System.Serializable]
    private class TheHighScore
    {
        public float score;
    }
}
