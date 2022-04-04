using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MealPoint : MonoBehaviour, IPoint
{
    private bool isBusy=false;
    
    public bool IsBusy
    {
        get => isBusy;
        set => isBusy = value;
    }

    public void Release()
    {
        isBusy = false;
    }
    
    public void Take()
    {
        isBusy = true;
    }
}
