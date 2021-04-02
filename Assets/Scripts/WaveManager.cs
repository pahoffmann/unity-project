using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject _coronaPrefab;
    [SerializeField] private GameObject _b117Prefab;
    [SerializeField] private GameObject _bossPrefab;
    //todo: more prefabs?
    
    // stores information about the levels
    public List<Level> levels = new List<Level>();

    private int _currentLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        ReadLevelFiles();

        StartCoroutine(WaveCoroutine(1));
    }

    // Update is called once per frame
    void Update()
    {
        // if not yet every virus of current wave has been spawned && timer is ok
            // spawn
            //adjust timer
        // else if every virus has been destroyed
            // continue to next wave (e.g. remove first element of list)
            // display end text
            // display start text
        // else
            //do nothin bitch
    }

    IEnumerator WaveCoroutine(int curLevel)
    {
        Debug.Log("Level:" + curLevel);
        Debug.Log("Num Levels: " + levels.Count);
        int levelIndex = curLevel - 1;

        // while we are not done spawning
        while (_currentLevel == curLevel && levels[levelIndex].SpawnOrder.Count > 0)
        {
            var toBeSpawned = levels[levelIndex].SpawnOrder.Dequeue();
            SpawnVirusPrefab(toBeSpawned);
            yield return new WaitForSeconds(levels[levelIndex].TimeBetweenSpawns);
        }
    }

    void SpawnVirusPrefab(String form)
    {
        switch (form)
        {
            case Constants.VirusForms.CoronaB117:
                Instantiate(_b117Prefab, new Vector3(Random.Range(-5f, 5f), 8f, 0f),Quaternion.identity, this.transform);
                break;
            case Constants.VirusForms.CoronaBase:
                Instantiate(_coronaPrefab, new Vector3(Random.Range(-5f, 5f), 8f, 0f), 
                    Quaternion.identity, this.transform);
                break;
            default: //add more virus spawns
                break;
        }
    }

    void ReadLevelFiles()
    {
        foreach (var file in Directory.EnumerateFiles("Assets/Levels"))
        {
            Debug.Log(file);
            // if the file does not start with the appropriate sequence we skip it, as it is not considered a lvl file
            if (!file.Contains(Constants.levelFileBase) || file.Contains("meta"))
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

                switch (token)
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
                        var spawnParts = value.Split(';');
                        //iterate through each of the parts

                        foreach (var part in spawnParts)
                        {
                            var virus = part.Split('-')[0];
                            int.TryParse(part.Split('-')[1], out var number);
                            
                            // add defined spawnorder to queue in level
                            switch (virus)
                            {
                                case Constants.VirusForms.CoronaBase:
                                    Debug.Log("Its corona");
                                    for (int i = 0; i < number; i++)
                                    {
                                        level.SpawnOrder.Enqueue(Constants.VirusForms.CoronaBase);
                                    }
                                    break;
                                
                                case Constants.VirusForms.CoronaB117:
                                    Debug.Log("Its b117");
                                    for (int i = 0; i < number; i++)
                                    {
                                        level.SpawnOrder.Enqueue(Constants.VirusForms.CoronaB117);
                                    }
                                    break;
                                // add if more virusses m8
                            }
                        }
                        
                        break;
                    default:
                        break;
                }  
            }
            
            levels.Add(level);
        }
    }
}
