using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    private int _score = 0;
    private string name;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _livesText;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private TextMeshProUGUI _inputText;
    public Text displayText;
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

    public void GameOver(bool gameOver)
    {
        gameUI.SetActive(false);
        gameOverScreen.SetActive(true);
        //_gameOverText.enabled = gameOver;
        //_gameOverText.text = "Game Over\nYour Score: " + _score;
    }

    public void toMainMenu()
    {
        name = _inputText.text;
        SceneManager.LoadScene("Menu");
    }
    
}
