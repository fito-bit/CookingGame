using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameConfig: MonoBehaviour
{
    [SerializeField] int startMealsCount;
    [SerializeField] int startGuestsCount;
    [SerializeField] int gameTime;
    [SerializeField][Range(1,3)] private int maxMealsInOrder;
    
    private int remainingGuestsCount;
    List<Guest> guests = new List<Guest>();
    int remainingMealsCount;
    private Config config;
    
    public List<Guest> Guests => guests;
    public int RemainingMealsCount
    {
        get => remainingMealsCount;
        set => remainingMealsCount = value;
    }
    public int RemainingGuestsCount
    {
        get => remainingGuestsCount;
        set => remainingGuestsCount = value;
    }

    public int MAXMealsInOrder => maxMealsInOrder;
    public int StartGuestsCount=>startGuestsCount;
    public int GameTime=>gameTime;
    public static GameConfig instance;

    public Config Configuration => config;


    private void Awake()
    {
        SaveSystem.Init();
        config = JsonUtility.FromJson<Config>(SaveSystem.Load());
        if (config == null)
        {
            config = new Config{
                startMealsCount=33,
                startGuestsCount=10,
                gameTime=50,
                maxMealsInOrder = 3
            };
            SaveSystem.Save(JsonUtility.ToJson(config));
        }
        startMealsCount = config.startMealsCount;
        startGuestsCount = config.startGuestsCount;
        gameTime = config.gameTime;
        maxMealsInOrder = config.maxMealsInOrder;
        
        instance = this;
        remainingGuestsCount = startGuestsCount;
        remainingMealsCount = startMealsCount;
    }
}
