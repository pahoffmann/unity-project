using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    private int _score = 0;
    [SerializeField] private Text _scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + _score;
        
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
}
