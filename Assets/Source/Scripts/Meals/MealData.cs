using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New MealData", menuName = "Meal Data", order = 52)]
public class MealData : ScriptableObject
{
    [SerializeField] private MealType mealType;
    [SerializeField] private Sprite iconSprite;
    [SerializeField] private float respawnTime;
    [SerializeField] private GameObject mealPrefab;
    
    public GameObject MealPrefab=>mealPrefab;
    public MealType MealType => mealType;
    public Sprite IconSprite => iconSprite;
    public float RespawnTime => respawnTime;
}
