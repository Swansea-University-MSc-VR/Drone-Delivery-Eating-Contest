using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour
{
    public Drone drone; 
    private Rigidbody _rigidbody;
    private GameObject _mouth;
    public float burgerVelocity;

    private void Start() 
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
        drone = transform.root.GetComponent<Drone>();
      //  _mouth = GameObject.FindGameObjectWithTag("Mouth");
    }

    public void Release()
    {
        transform.parent = null;
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        
        // add force to rigidbody along forward vector
        _rigidbody.AddForce(transform.forward * burgerVelocity, ForceMode.Impulse);
    }

    public void Reset()
    {
        _rigidbody.isKinematic = true;
        transform.parent = drone.attachPoint.transform;
        transform.localPosition = Vector3.zero;

    }
}
