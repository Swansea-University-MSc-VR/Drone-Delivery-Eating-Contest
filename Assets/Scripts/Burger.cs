using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour
{
    public Drone drone; 
    private Rigidbody _rigidbody;

    private void Start() 
    {
        _rigidbody = GetComponent<Rigidbody>();
        drone = transform.root.GetComponent<Drone>();
    }

    public void Release()
    {
        _rigidbody.isKinematic = false;
        transform.parent = null;
    }

    public void Reset()
    {
        _rigidbody.isKinematic = true;
        transform.parent = drone.attachPoint.transform;
        transform.localPosition = Vector3.zero;

    }
}
