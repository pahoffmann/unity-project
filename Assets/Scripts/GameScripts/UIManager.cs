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
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _livesText;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private TextMeshProUGUI _inputTextGameOver;
    [SerializeField] private TextMeshProUGUI _inputTextWin;
    [SerializeField] private TextMeshProUGUI _gameOverScore;
    [SerializeField] private TextMeshProUGUI _winnerScore;
    [SerializeField] private AudioClip _gameWinnerAudio;
    public GameObject gameOverScreen;
    public GameObject gameUI;
    public GameObject youWin;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + _score;
        gameOverScreen.SetActive(false);
        youWin.SetActive(false);
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

    public void GameOver(bool gameOver)
    {
        gameUI.SetActive(false);
        youWin.SetActive(false);
        gameOverScreen.SetActive(true);
        _gameOverScore.text = "Score: " + _score;
        //_gameOverText.enabled = gameOver;
        //_gameOverText.text = "Game Over\nYour Score: " + _score;
    }

    public void GameWinner()
    {
        gameUI.SetActive(false);
        gameOverScreen.SetActive(false);
        youWin.SetActive(true);
        _winnerScore.text = "Score: " + _score;
        AudioSource.PlayClipAtPoint(_gameWinnerAudio, new Vector3(0,0,0));
    }

    public void toMainMenu()
    {
        // if the player entered a name, we save the score
        if (_inputTextGameOver.text.Replace("\u200B", "").Length > 0)
        {
            File.AppendAllText(Application.streamingAssetsPath + "/" + Constants.highscoreFile,  _inputTextGameOver.text.Trim().Replace("\u200B", "") + "-" + _score + Environment.NewLine);
        }
        else if (_inputTextWin.text.Replace("\u200B", "").Length > 0)
        {
            File.AppendAllText(Application.streamingAssetsPath + "/" + Constants.highscoreFile,  _inputTextWin.text.Trim().Replace("\u200B", "") + "-" + _score + Environment.NewLine);
        }
        
        SceneManager.LoadScene("Menu");
    }
}
