using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    //maybe add prefab and spawning methods to a utility class
    
    //virus prefabs
    [SerializeField] private GameObject _coronaPrefab;
    [SerializeField] private GameObject _b117Prefab;
    [SerializeField] private GameObject _484KPrefab;
    [SerializeField] private GameObject _b1128Prefab;
    [SerializeField] private GameObject _n501yPrefab;
    [SerializeField] private GameObject _p681hPrefab;
    [SerializeField] private GameObject _y144Prefab;
    [SerializeField] private GameObject _bossPrefab;
    //todo: more prefabs?
    
    
    //powerup prefabs
    [SerializeField] private GameObject _uVLightPowerUp;

    // UIManager shit
    [SerializeField] private UIManager _UIManager;
    
    // stores information about the levels
    public List<Level> levels = new List<Level>();

    private int _currentLevel = 1;

    private bool _doneSpawning = false;

    private bool _gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        ReadLevelFiles();

        StartCoroutine(WaveCoroutine(_currentLevel));
    }

    /// <summary>
    /// Handles the game logic
    /// </summary>
    void Update()
    {
        if (_gameOver)
        {
            _UIManager.GameOver(true);
            
            // wait or do something else, you know, like hitting sebastian or smth
            
            //SceneManager.LoadScene("Menu");
        }
        // all virusses destroyed
        if (transform.childCount == 0 && _doneSpawning)
        {
            // TODO: display lvl done message
            
            _doneSpawning = false; // reset
            
            //all levels done?
            if (_currentLevel == levels.Count)
            {
                _UIManager.GameOver(true);
            }
            else
            {
                // increase current level and start next level
                _currentLevel++;
                StartCoroutine(WaveCoroutine(_currentLevel));
            }
        }
    }
    
    /// <summary>
    /// Wave Coroutine spawns one level (wave) until every virus and powerup is destroyed
    /// </summary>
    /// <param name="curLevel"></param>
    /// <returns></returns>
    IEnumerator WaveCoroutine(int curLevel)
    {
        // TODO: display level start message

        int levelIndex = curLevel - 1;

        // while we are not done spawning
        while (_currentLevel == curLevel && levels[levelIndex].SpawnOrder.Count > 0)
        {
            var toBeSpawned = levels[levelIndex].SpawnOrder.Dequeue();
            SpawnPrefab(toBeSpawned);
            yield return new WaitForSeconds(levels[levelIndex].TimeBetweenSpawns);
        }

        _doneSpawning = true;
    }
    
    /// <summary>
    /// Spawns the prefab specified by the form.
    /// </summary>
    /// <param name="form">Form of the virus or powerup, defined in the Constants.cs class</param>
    void SpawnPrefab(String form)
    {
        switch (form)
        {
            case Constants.VirusForms.CoronaB117:
                Instantiate(_b117Prefab, new Vector3(Random.Range(Constants.Dimensions.BorderLeft, Constants.Dimensions.BorderRight), 8f, 0f),Quaternion.identity, this.transform);
                break;
            case Constants.VirusForms.CoronaBase:
                Instantiate(_coronaPrefab, new Vector3(Random.Range(Constants.Dimensions.BorderLeft, Constants.Dimensions.BorderRight), 8f, 0f), 
                    Quaternion.identity, this.transform);
                break;
            case Constants.VirusForms.Corona484K:
                Instantiate(_484KPrefab, new Vector3(Random.Range(Constants.Dimensions.BorderLeft, Constants.Dimensions.BorderRight), 8f, 0f), 
                    Quaternion.identity, this.transform);
                break;
            case Constants.VirusForms.CoronaB1128:
                Instantiate(_b1128Prefab, new Vector3(Random.Range(Constants.Dimensions.BorderLeft, Constants.Dimensions.BorderRight), 8f, 0f), 
                    Quaternion.identity, this.transform);
                break;
            case Constants.VirusForms.CoronaY144:
                Instantiate(_y144Prefab, new Vector3(Random.Range(Constants.Dimensions.BorderLeft, Constants.Dimensions.BorderRight), 8f, 0f), 
                    Quaternion.identity, this.transform);
                break;
            case Constants.VirusForms.CoronaN501Y:
                Instantiate(_n501yPrefab, new Vector3(Random.Range(Constants.Dimensions.BorderLeft, Constants.Dimensions.BorderRight), 8f, 0f), 
                    Quaternion.identity, this.transform);
                break;
            case Constants.VirusForms.CoronaP681H:
                Instantiate(_p681hPrefab, new Vector3(Random.Range(Constants.Dimensions.BorderLeft, Constants.Dimensions.BorderRight), 8f, 0f), 
                    Quaternion.identity, this.transform);
                break;
            case Constants.PowerUps.UVLight:
                Instantiate(_uVLightPowerUp, new Vector3(Random.Range(Constants.Dimensions.BorderLeft, Constants.Dimensions.BorderRight), 8f, 0f), 
                    Quaternion.identity, this.transform);
                break;
            default: //add more virus spawns
                break;
        }
    }
    
    
    /// <summary>
    /// Handler called, if the player dies.
    /// </summary>
    public void onPlayerDeath()
    {
        // when the player is dead, we stop all spawning, and the game is over
        StopAllCoroutines();
        _gameOver = true;
        
        // destroy every left children of the wave manager
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }
    
    /// <summary>
    /// Reads all levels specified in the level folder. Handles errors.
    /// </summary>
    void ReadLevelFiles()
    {
        foreach (var file in Directory.EnumerateFiles(Application.streamingAssetsPath + "/Levels"))
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
                        //this parsing needs to be done, as c# is retarded.
                        value = value.Replace(',', '.');
                        float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out float tbs);
                        level.TimeBetweenSpawns = tbs;
                        Debug.Log("tbs: " + tbs);
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
                                case Constants.VirusForms.Corona484K:
                                    Debug.Log("Its corona");
                                    for (int i = 0; i < number; i++)
                                    {
                                        level.SpawnOrder.Enqueue(Constants.VirusForms.Corona484K);
                                    }
                                    break;
                                case Constants.VirusForms.CoronaB1128:
                                    Debug.Log("Its corona");
                                    for (int i = 0; i < number; i++)
                                    {
                                        level.SpawnOrder.Enqueue(Constants.VirusForms.CoronaB1128);
                                    }
                                    break;
                                case Constants.VirusForms.CoronaY144:
                                    Debug.Log("Its corona");
                                    for (int i = 0; i < number; i++)
                                    {
                                        level.SpawnOrder.Enqueue(Constants.VirusForms.CoronaY144);
                                    }
                                    break;
                                case Constants.VirusForms.CoronaN501Y:
                                    Debug.Log("Its corona");
                                    for (int i = 0; i < number; i++)
                                    {
                                        level.SpawnOrder.Enqueue(Constants.VirusForms.CoronaN501Y);
                                    }
                                    break;
                                case Constants.VirusForms.CoronaP681H:
                                    Debug.Log("Its corona");
                                    for (int i = 0; i < number; i++)
                                    {
                                        level.SpawnOrder.Enqueue(Constants.VirusForms.CoronaP681H);
                                    }
                                    break;
                                case Constants.VirusForms.CoronaB117:
                                    Debug.Log("Its b117");
                                    for (int i = 0; i < number; i++)
                                    {
                                        level.SpawnOrder.Enqueue(Constants.VirusForms.CoronaB117);
                                    }
                                    break;
                                case Constants.PowerUps.UVLight:
                                    for (int i = 0; i < number; i++)
                                    {
                                        level.SpawnOrder.Enqueue(Constants.PowerUps.UVLight);
                                    }
                                    break;
                                default:
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