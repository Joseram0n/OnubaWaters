using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public static class SaveSystem
{
    public static void SavePlayer(PlayerScore score)
    {
        string path = Application.persistentDataPath + "/player.txt";
        if(!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(score.ToString());
            }
        }
        else
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(score.ToString());
            }
        }
    }
    public static List<PlayerScore> LoadPlayers()
    {
        string path = Application.persistentDataPath + "/player.txt";
        if(File.Exists(path))
        {
            List<string> fileLines = File.ReadAllLines(path).ToList();
            List<PlayerScore> players=new List<PlayerScore>();
            foreach (string p in fileLines){
                players.Add(parseScore(p));
            }
            return players;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static PlayerScore parseScore(string score)
    {
        Debug.LogWarning("String: " + score);
        string[] prov = score.Split('-');
        Debug.LogWarning("Substrings: " + prov[0] + " " + prov[1]);
        int scoreP = int.Parse(prov[0]);
        string name = prov[1];
        return new PlayerScore(scoreP,name);
    }
}
