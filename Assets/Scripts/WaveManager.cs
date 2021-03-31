using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject _coronaPrefab;
    [SerializeField] private GameObject _bossPrefab;
    //todo: more prefabs?
    
    
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
        foreach (var file in Directory.EnumerateFiles("Assets/Levels"))
        {
            // if the file does not start with the appropriate sequence we skip it, as it is not considered a lvl file
            if (!file.StartsWith(Constants.levelFileBase))
            {
                continue;
            }
            
            var lines = File.ReadLines(file);
            Level level = new Level();
            
            foreach (var line in lines)
            {
                var split = line.Split(':');
                
                //when there is more than to elements after the split, the files format is wrong.
                if (split.Length != 2)
                {
                    continue;
                }

                var token = split[0];
                var value = split[1];
                
                switch (split[0])
                {
                    case Constants.LevelConstants.Type:
                        var type = value;
                        
                        //level has to have a valid type
                        if (type != Constants.LevelTypes.Normal && type != Constants.LevelTypes.Boss)
                        {
                            continue;
                        }

                        level.Type = type;
                        
                        break;
                    case Constants.LevelConstants.Num:
                        int.TryParse(value, out int num);
                        level.LvlNum = num;
                        break;
                    case Constants.LevelConstants.EndDescription:
                        level.EndDescription = value;
                        break;
                    case Constants.LevelConstants.StartupDescription:
                        level.StartupDescription = value;
                        break;
                    case Constants.LevelConstants.TimeBetweenSpawns:
                        int.TryParse(value, out int tbs);
                        level.TimeBetweenSpawns = tbs;
                        break;
                    case Constants.LevelConstants.NumEnemies:
                        //todo: parse complicated case, how to properly use prefabs here
                        break;
                    default:
                        break;
                }  
            }
        }
    }
}
