using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject resultWindow;
    [SerializeField] private Text resultText;
    [SerializeField] private Text timer;
    [SerializeField] private Text guestCount;
    public static UIManager instance;
    private float currentTime;
    private float startTime;

    private void Start()
    {
        instance = this;
        startTime = GameConfig.instance.GameTime;
        currentTime = startTime;
        StartCoroutine(Timer());
        SetGuests();
    }

    public void Lose()
    {
        Result("GAME OVER");
    }
    public void Win()
    {
        Result("VICTORY");
    }

    void Result(string text)
    {
        StopAllCoroutines();
        resultText.text = text;
        resultWindow.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator Timer()
    {
        while (currentTime>0)
        {
            currentTime -= Time.deltaTime;
            int minutes = (int)currentTime / 60;
            timer.text = minutes+ " : " +  Convert.ToInt32(currentTime % 60);
            yield return new WaitForFixedUpdate();
        }
        Debug.LogError(1);
        Lose();
    }

    public void SetGuests()
    {
        guestCount.text = GameConfig.instance.RemainingGuestsCount + "/" + GameConfig.instance.StartGuestsCount;
    }
}
