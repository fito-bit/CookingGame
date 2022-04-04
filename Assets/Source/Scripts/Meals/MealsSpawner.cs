using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MealsSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] redMealPoints;
    [SerializeField] private Transform[] greenMealPoints;
    [SerializeField] private Transform[] yellowMealPoints;
    [SerializeField] private Transform[] blueMealPoints;
    [SerializeField] private Pool[] pools;
    
    public static MealsSpawner instance;

    private void Start()
    {
        instance = this;
        StartCoroutine(SpawnLine(yellowMealPoints,pools[0]));
        StartCoroutine(SpawnLine(redMealPoints,pools[1]));
        StartCoroutine(SpawnLine(blueMealPoints,pools[2]));
        StartCoroutine(SpawnLine(greenMealPoints,pools[3]));
    }

    public void Respawn(Transform point,GameObject obj)
    {
        foreach (var pool in pools)
        {
            if (pool.Sample == obj)
            {
                StartCoroutine(RespawnCoroutine(pool,point));
                return;
            }
        }
    }
    
    IEnumerator RespawnCoroutine(Pool pool, Transform point)
    {
        yield return new WaitForSeconds(pool.Sample.GetComponent<Meal>().MealData.RespawnTime);
        if (CheckSpawnPossibility(point))
            Spawn(pool,point);
    }

    void Spawn(Pool pool,Transform point)
    {
        Meal meal=pool.GetObject().GetComponent<Meal>();
        meal.transform.position = point.position;
        meal.transform.rotation = point.rotation;
        meal.gameObject.SetActive(true);
        MealPoint mealPoint = point.GetComponent<MealPoint>();
        meal.BornPoint = mealPoint;
        mealPoint.IsBusy = true;
    }

    bool CheckSpawnPossibility(Transform point)
    {
        return !point.GetComponent<MealPoint>().IsBusy;
    }

    IEnumerator SpawnLine(Transform[] points,Pool pool)
    {
        foreach (var point in points)
        {
            if (CheckSpawnPossibility(point))
            {
                Spawn(pool,point);
            }
            yield return new WaitForSeconds(pool.Sample.GetComponent<Meal>().MealData.RespawnTime);
        }
    }
}
