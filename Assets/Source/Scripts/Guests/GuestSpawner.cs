using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GuestSpawner : MonoBehaviour
{
    [SerializeField] public Transform[] spawnPoints;
    [SerializeField] public Transform[] waitPoints;
    [SerializeField] private Pool guestPool;
    
    private int spawnedGuests = 0;
    public static GuestSpawner instance;

    public void Spawn()
    {
        if (spawnedGuests < GameConfig.instance.StartGuestsCount&& GameConfig.instance.RemainingMealsCount>0)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Guest guest = guestPool.GetObject().GetComponent<Guest>();
            guest.AddMeals();
            guest.gameObject.transform.position = spawnPoint.position;
            guest.gameObject.SetActive(true);
            guest.BornPoint = spawnPoint;
            int index = Random.Range(0, waitPoints.Length);
            GuestPoint guestPoint=waitPoints[index].GetComponent<GuestPoint>();
            while (guestPoint.IsBusy)
            {
                index = Random.Range(0, waitPoints.Length);
                guestPoint = waitPoints[index].GetComponent<GuestPoint>();
            }
            guestPoint.Take();
            guest.MoveToWaitPoint(waitPoints[index]);
            spawnedGuests++;
        }
    }

    IEnumerator SpawnGuests(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Spawn();
        }
    }

    private void Start()
    {
        instance = this;
        StartCoroutine(SpawnGuests(4));
    }
}
