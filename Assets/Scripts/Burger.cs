using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour
{
    public Drone drone; 
    private Rigidbody _rigidbody;
    private Collider _collider;
    private GameObject _mouth;
    public float burgerVelocity;
    private AudioSource _audioSource;
    public float perfectTimer;
    public bool isPerfect;

    private void Start() 
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _collider = GetComponent<Collider>();
        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
        drone = transform.root.GetComponent<Drone>();
    }

    public void Release()
    {
        _collider.enabled = true;
        transform.parent = null;
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        
        // add force to rigidbody along forward vector
        _rigidbody.AddForce(transform.forward * burgerVelocity, ForceMode.Impulse);

        isPerfect = true;
    }

    private IEnumerator PerfectTimer()
    {
        yield return new WaitForSeconds(perfectTimer);
        isPerfect = false;
    }

    public void Eat()
    {
        _collider.enabled = false;
        _audioSource.Play();
        Reset();
    }

    public void Reset()
    {
        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
        transform.parent = drone.attachPoint.transform;
        transform.localPosition = Vector3.zero;
        isPerfect = true;
    }
}