using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    private int _score = 0;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _livesText;
    [SerializeField] private Text _gameOverText;
    
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + _score;
        GameOver(false);
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
        _gameOverText.enabled = gameOver;
        _gameOverText.text = "Game Over\nYour Score: " + _score;
    }
}
