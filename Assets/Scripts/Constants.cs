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

    public readonly struct Dimensions
    {
        public static float BorderLeft   = -9.5f;
        public static float BorderRight  = 9.5f;
        public static float BorderTop    = 4.5f;
        public static float BorderBottom = -4.5f;
    }

    public static String levelFileBase = "lvl";
}