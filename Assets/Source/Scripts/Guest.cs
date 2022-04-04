using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Guest : MonoBehaviour
{
    [SerializeField]private List<Meal> meals=new List<Meal>();
    private float currentTime;
    private GuestPoint waitPoint;
    [SerializeField]private float waitTime;
    [SerializeField]private float walkTime;
    [SerializeField] private Meal[] mealPrefabs;
    public List<Meal> Meals => meals;
    Transform bornPoint;

    public Transform BornPoint
    {
        set => bornPoint = value;
    }

    public void AddMeals()
    {
        if (GameConfig.instance.RemainingMealsCount >= GameConfig.instance.MAXMealsInOrder&& GameConfig.instance.MAXMealsInOrder<=3)
        {
            AddMeals(GameConfig.instance.MAXMealsInOrder);
        }else
        {
            AddMeals(1);
        }
    }

    public void RemoveMeal(int mealIndex)
    {
        waitPoint.GuestPanel.DisableIcon(meals[mealIndex].MealData.IconSprite);
        meals.Remove(meals[mealIndex]);
        if (meals.Count == 0)
        {
            GameConfig.instance.RemainingGuestsCount--;
            UIManager.instance.SetGuests();
            if (GameConfig.instance.RemainingGuestsCount == 0)
            {
                UIManager.instance.Win();
            }
            StopAllCoroutines();
            Leave();
        }
    }

    void AddMeals(int index)
    {
        int count = Random.Range(1, index+1);
        GameConfig.instance.RemainingMealsCount -= count;
        for (int i = 0; i < count; i++)
        {
            meals.Add(mealPrefabs[Random.Range(0,mealPrefabs.Length)]);
        }
    }

    IEnumerator GuestWait()
    {
        GameConfig.instance.Guests.Add(this);
        yield return new WaitForSeconds(waitTime);
        UIManager.instance.Lose();
        //Leave();
    }
    
    IEnumerator Arrive()
    {
        yield return new WaitForSeconds(walkTime);
        waitPoint.GuestArrived(waitTime,this);
        StartCoroutine(GuestWait());
    }

    void Disable()
    {
        meals.Clear();
        this.gameObject.SetActive(false);
    }
    
    void Leave()
    {
        waitPoint.Release();
        GuestSpawner.instance.Spawn();
        GameConfig.instance.Guests.Remove(this);
        transform.DOMove(bornPoint.position, walkTime).OnComplete(Disable);
    }

    public void MoveToWaitPoint(Transform targetPoint)
    {
        waitPoint =targetPoint.GetComponent<GuestPoint>();
        StartCoroutine(Arrive());
        transform.DOMove(targetPoint.position, walkTime);
    }
}
