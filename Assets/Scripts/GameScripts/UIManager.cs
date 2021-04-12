using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    private int _score = 0;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _livesText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _levelMessageText;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private TextMeshProUGUI _inputText;
    [SerializeField] private TextMeshProUGUI _gameOverScore;
    [SerializeField] private GameObject _musicPlayer;
    public GameObject gameOverScreen;
    public GameObject gameUI;
    
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + _score;
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int score)
    {
        _score += score;
        _scoreText.text = "Score: " + _score;
    }

    public void setLives(int lives)
    {
        _livesText.text = "Lives: " + lives;
    }

    public void setLevel(int level)
    {
        _levelText.text = "Level: " + level;
    }
    
    public void GameOver(bool gameOver)
    {
        gameUI.SetActive(false);
        gameOverScreen.SetActive(true);
        _gameOverScore.text = "Score: " + _score;
        //_gameOverText.enabled = gameOver;
        //_gameOverText.text = "Game Over\nYour Score: " + _score;
    }

    public void toMainMenu()
    {
        // if the player entered a name, we save the score
        if (_inputText.text.Length > 0)
        {
            File.AppendAllText(Application.streamingAssetsPath + "/" + Constants.highscoreFile,  _inputText.text + "-" + _score + Environment.NewLine);
        }
        SceneManager.LoadScene("Menu");
    }

    public void setLevelMessage(String message, int duration)
    {
        StartCoroutine(LevelMessageCoroutine(message, duration));
    }

    public void setMusic(bool musicActive)
    {
        _musicPlayer.SetActive(false);
    }
    
    IEnumerator LevelMessageCoroutine(String message, int duration)
    {
        yield return new WaitForSeconds(1);
        _levelMessageText.text = message;
        _levelMessageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration - 1);
        _levelMessageText.gameObject.SetActive(false);
    }
}
