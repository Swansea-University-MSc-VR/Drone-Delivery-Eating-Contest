using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using Pixelplacement.TweenSystem;
using Oculus.Interaction;

public class Drone : MonoBehaviour
{
    public Transform attachPoint;
    public Burger burgerPrefab;
    public Burger burger;
    public bool burgerReleased;
    public float burgerReleasePercentage;


    public Drone OtherDrone;
    public bool otherDroneNotified;
    public float otherDroneNotifiedPercentage;

    public Spline[] splines;
    public float flightDuration;    
    private float flightPercentage;

    private TweenBase tween;
  
    public void Fly()
    {
        if (burger != null)
        {
            Destroy(burger.gameObject);
        }
        
        burger = Instantiate(burgerPrefab, attachPoint);
        burger.transform.localPosition = Vector3.zero;
        burgerReleased = false;
        otherDroneNotified = false;

        Spline spline = splines[Random.Range(0, splines.Length)];

        if (tween != null)
            tween.Cancel();

        tween = Pixelplacement.Tween.Spline(spline, transform, 0, 1, true, flightDuration, 0, Pixelplacement.Tween.EaseLinear, Pixelplacement.Tween.LoopType.None);
    }

    public void ReleaseBurger()
    {
        burger.Release();
        burgerReleased = true;
    }
    
    private void Update()
    {
        if (tween == null)
            return;

        flightPercentage = tween.Percentage;

        if (flightPercentage > burgerReleasePercentage && burgerReleased == false)
            ReleaseBurger();

        if (flightPercentage > otherDroneNotifiedPercentage && otherDroneNotified == false)
        {
            OtherDrone.gameObject.SetActive(true);
            OtherDrone.Fly();
            otherDroneNotified = true;
        }

        if (flightPercentage > 0.99f)
            gameObject.SetActive(false);
    }
}
