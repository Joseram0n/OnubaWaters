using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameVariables
{
    public static float Volume { get; set; } = 0.2f;
    public static Resolution Resolution { get; set; }
    public static string Name { get; set; }
    public static int Gridsize { get; set; }
    public static bool playGenetic { get; set; }
    
    /*
    public static void setVolume(float vol)
    {
        Volume = vol;
    }
    public static void setResolution(Resolution resol)
    {
        resolution = resol;
    }
    public static float getVolume()
    {
        return Volume;
    }
    public static Resolution getResolution()
    {
        return resolution;
    }
    public static void setName(string nombre)
    {
        name = nombre;
    }
    public static string getName()
    {
        return name;
    }
    public static void setGrid(int grid)
    {
        gridsize = grid;
    }
    public static int getGrid()
    {
        return gridsize;
    }
    */
}
