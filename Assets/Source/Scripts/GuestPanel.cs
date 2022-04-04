using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuestPanel : MonoBehaviour
{
    [SerializeField] private Image[] mealIcons;
    [SerializeField] private Image timerImg;
    private float startTime;
    private float currentTime;
    
    
    IEnumerator TimeBar()
    {
        currentTime = startTime;
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            timerImg.fillAmount = currentTime/startTime;
            yield return new WaitForFixedUpdate();
        }
    }

    public void DisableIcon(Sprite sprite)
    {
        for (int i = 0; i < mealIcons.Length; i++)
        {
            if (mealIcons[i].sprite == sprite)
            {
                mealIcons[i].sprite = null;
                mealIcons[i].enabled = false;
                return;
            }
        }
    }
    
    public void SetPanel(float time,List<Meal> meals)
    {
        startTime = time;
        timerImg.fillAmount = 1;
        StartCoroutine(TimeBar());
        for (int i = 0; i < meals.Count; i++)
        {
            mealIcons[i].enabled = true;
            mealIcons[i].sprite = meals[i].MealData.IconSprite;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < mealIcons.Length; i++)
        {
            mealIcons[i].sprite = null;
            mealIcons[i].enabled = false;
        }
        this.gameObject.SetActive(false);
    }
}
