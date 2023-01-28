using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
public class Drone : MonoBehaviour
{
    public Transform attachPoint;
    public Burger burger;

    public Spline spline;
    public float flightDuration;


    private void Start()
    {
      burger = attachPoint.transform.GetChild(0).GetComponent<Burger>();         
      Fly();       
    }

    public void Fly()
    {
        Tween.Spline(spline, transform, 0, 1, true, flightDuration, 0, Tween.EaseLinear, Tween.LoopType.None);
    }

    public void ReleaseBurger()
    {
        burger.Release();
    }
}
