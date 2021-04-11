using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class Highscores : MonoBehaviour
{
    struct HighScore
    {
        public string playerName { get; set; }
        public int score { get; set; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        var lines = File.ReadLines(Application.streamingAssetsPath + "/" + Constants.highscoreFile);
        List<HighScore> highscoreList = new List<HighScore>();
        
        foreach (var line in lines)
        {
            Debug.Log(line);
            var split = line.Split('-');
            var player = split[0];
            var score = split[1];

            HighScore highScore = new HighScore();
            highScore.score = int.Parse(score);
            highScore.playerName = player;
            highscoreList.Add(highScore);
        }
        
        // order by value instead of ordering by key
        highscoreList.Sort(delegate(HighScore score1, HighScore score2)
        {
            if (score1.score > score2.score)
            {
                return -1;
            }
            else return 1;
        });
        
        
        // get the appropriate children of the highscore menu and set the text
        int maxIndex = Math.Min(highscoreList.Count(), 10);
        
        for (int i = 0; i < maxIndex; i++)
        {
            var child = transform.GetChild(i); // get i-th highscore text child
            if (!child.name.Contains((i+1).ToString()))
            {
                throw new DataException("HighscoreText Objects in HighscoreMenu are in incorrect order");
            }

            TextMeshProUGUI textComp = child.GetComponent<TextMeshProUGUI>();
            textComp.text = i + 1 + ". " + highscoreList[i].playerName + " | " + highscoreList[i].score;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
