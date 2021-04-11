using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Level class containing all the relevant information needed in a level
 */
public class Level
{
    public Level(int lvlNum, float timeBetweenSpawns, String startupDescription, String endDescription)
    {
        LvlNum = lvlNum;
        TimeBetweenSpawns = timeBetweenSpawns;
        StartupDescription = startupDescription;
        EndDescription = endDescription;
        SpawnOrder = new Queue<string>();
    }

    public Level()
    {
        SpawnOrder = new Queue<string>();
    }

    public int LvlNum { get; set; }
    public float TimeBetweenSpawns { get; set; }
    public String StartupDescription { get; set; }
    public String EndDescription { get; set; }
    public String Type { get; set; }
    
    public Queue<String> SpawnOrder { get; set; }
}
