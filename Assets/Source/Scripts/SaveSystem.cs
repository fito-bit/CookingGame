using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }
    
    public static string Load()
    {
        if (File.Exists(SAVE_FOLDER + "save.json"))
        {
            string saveString =
                File.ReadAllText(SAVE_FOLDER + "save.json");
            return saveString;
        }
        else
        {
            return null;
        }
    }
    
    public static void Save(string saveString)
    {
        File.WriteAllText(SAVE_FOLDER+"save.json",saveString);
    }
}
