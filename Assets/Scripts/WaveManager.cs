using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<Level> levels;
    // Start is called before the first frame update
    void Start()
    {
        ReadLevelFiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReadLevelFiles()
    {
        String lvlPath = Constants.levelFileBase;

        foreach (var file in Directory.EnumerateFiles("Assets/Levels"))
        {
            string contents = File.ReadAllText(file);
            Debug.LogError(contents);
        }
    }
}
