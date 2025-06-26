using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    //Getting the text of scores
    public TMP_Text touchdownText;
    public TMP_Text highscoreText;
    int level = 0;
    int highscore = 0;

    //levels from Levelmanager
    protected LevelManager _level;
    // Awake is called before Start method
    private void Awake()
    {
        instance = this;   
    }
    // Start is called before the first frame update
    void Start()
    {
        //retrieving varaibles from LevelManager
        _level = GetComponent<LevelManager>();
        //initializing highscore
        highscore = PlayerPrefs.GetInt("highscore", 0);
        //setting the score text
        touchdownText.text = level.ToString() + " Touchdowns";
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
    }

    // Update is called once per frame
    public void addScore()
    {
        level = _level.getCurrentLevel() - 1;
        //setting the score text
        touchdownText.text = level.ToString() + " Touchdowns";
        if (highscore < level)
        {
            PlayerPrefs.SetInt("highscore", level);
        }
    }
}
