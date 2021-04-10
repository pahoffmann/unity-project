using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    // TODO Menu functionality
    


    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReadHighscores()
    {
        var lines = File.ReadLines(Application.streamingAssetsPath + "/" + Constants.highscoreFile);
        SortedDictionary<string, int> highscoreList = new SortedDictionary<string, int>();
        
        foreach (var line in lines)
        {
            var split = line.Split('-');
            var player = split[0];
            var score = split[1];
            
            highscoreList.Add(player, int.Parse(score));
        }
    }
}
