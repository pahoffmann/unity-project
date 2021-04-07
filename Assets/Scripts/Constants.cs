using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public readonly struct Tags
    {
        public static String Player = "Player";
        public static String Vaccine = "Vaccine";
        public static String Virus = "Virus";
        public static String UVLight = "UVLight";
    }
    
    public readonly struct VirusForms
    {
        public const String CoronaBase = "Coroner";
        public const String CoronaB117 = "B117";
        //insert more here
        //maybe use tags instead??
    }

    public readonly struct PowerUps
    {
        public const String UVLight = "UVLight";
    }

    public readonly struct Dimensions
    {
        public static float BorderLeft   = -9.5f;
        public static float BorderRight  = 9.5f;
        public static float BorderTop    = 4.5f;
        public static float BorderBottom = -4.5f;
    }
    
    /**
     * Constants for the file format used to enable levels.
     */
    public readonly struct LevelConstants
    {
        public const String Type = "type";
        public const String Num = "num";
        public const String StartupDescription = "msg1";
        public const String EndDescription = "msg2";
        public const String TimeBetweenSpawns = "tbs";
        public const String NumEnemies = "prefabs";
    }

    public readonly struct LevelTypes
    {
        public const String Normal = "normal";
        public const String Boss = "boss";
    }
    
    // base name of the levels
    public static String levelFileBase = "lvl";
}