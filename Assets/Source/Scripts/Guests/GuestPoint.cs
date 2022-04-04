using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GuestPoint : MonoBehaviour, IPoint
{ 
    [SerializeField] private GameObject guestPanelObj;
    private GuestPanel guestPanel;

    public GuestPanel GuestPanel => guestPanel;

    private bool isBusy=false;
    
    public bool IsBusy => isBusy;

    private void Start()
    {
        guestPanel = guestPanelObj.GetComponent<GuestPanel>();
    }

    public void Release()
    {
        isBusy = false;
        GuestLeave();
    }
    
    public void Take()
    {
        isBusy = true;
    }

    void GuestLeave()
    {
        guestPanelObj.SetActive(false);
    }

    public void GuestArrived(float time,Guest guest)
    {
        guestPanelObj.SetActive(true);
        guestPanel.SetPanel(time,guest.Meals);
    }
}
