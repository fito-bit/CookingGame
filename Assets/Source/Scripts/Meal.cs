using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Meal : MonoBehaviour
{
    [SerializeField] private MealData mealData;
    [SerializeField] private MeshRenderer meal;
    MealPoint bornPoint;
    public MealPoint BornPoint
    {
        set => bornPoint = value;
    }
    public MealData MealData => mealData;

    private void OnMouseDown()
    {
        int guestsCount = GameConfig.instance.Guests.Count;
        for (int i = 0; i < guestsCount; i++)
        {
            Guest guest = GameConfig.instance.Guests[i];
            for (int j = 0; j < guest.Meals.Count; j++)
            {
                if (guest.Meals[j].MealData.MealType == mealData.MealType)
                {
                    bornPoint.IsBusy = false;
                    MealsSpawner.instance.Respawn(bornPoint.transform, mealData.MealPrefab);
                    guest.RemoveMeal(j);
                    gameObject.SetActive(false);
                    return;
                }
            }
        }
    }
}
