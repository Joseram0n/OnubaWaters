using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerScore: System.IComparable<PlayerScore>
{
    public int score;
    public string name;
    public PlayerScore(int score, string name)
    {
        this.score = score;
        this.name = name;
    }
    public int getScore()
    {
        return score;
    }
    public string getName()
    {
        return name;
    }

    override
    public string ToString()
    {
        return "" +score + "-" + name;
    }

    public int CompareTo(PlayerScore sc)
    {
        if (sc == null)
            return 1;
        else
        {
            int puntuacion = sc.getScore();
            int mypt = score;
            return (puntuacion.CompareTo(mypt));
        }
    }
}
