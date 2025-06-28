using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TMP_Text pointsText;
    int score = 1;
    public LevelManager _level; //retrieve variables from level manager
    //making the player see the UI
    public void Setup()
    {
        score = _level.getCurrentLevel() - 1;
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " Touchdowns";

    }
    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
    public void leaderboardButton()
    {
        SceneManager.LoadScene("LeaderBoard");
        Time.timeScale = 1;

    }
}
