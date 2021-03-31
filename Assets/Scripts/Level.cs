using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * struct for all the different virus variants
 */
public struct VirusVariants
{
    public int numCorona { get; set;}
    public int numB117 { get; set; }
    // create more ???
}

/**
 * Level class containing all the relevant information needed in a level
 */
public class Level
{
    public Level(int lvlNum, float timeBetweenSpawns, String startupDescription, String endDescription, VirusVariants variants)
    {
        LvlNum = lvlNum;
        TimeBetweenSpawns = timeBetweenSpawns;
        StartupDescription = startupDescription;
        EndDescription = endDescription;
        Variants = variants;
    }

    public Level()
    {
        
    }

    public int LvlNum { get; set; }
    public float TimeBetweenSpawns { get; set; }
    public String StartupDescription { get; set; }
    public String EndDescription { get; set; }
    public VirusVariants Variants { get; set; }
}
