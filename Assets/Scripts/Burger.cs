using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.HandGrab;

public class Burger : MonoBehaviour
{
    public Drone drone; 
    private Rigidbody _rigidbody;
    private Collider _collider;
    private GameObject _mouth;
    public float burgerVelocity;
    private AudioSource _audioSource;
    public float maxPerfectTime = 1f;
    private WaitForSeconds _perfectWait;
    public bool isPerfect;

    public HandGrabInteractor leftHandGrabInteractor;
    public HandGrabInteractor rightHandGrabInteractor;

    private void Start() 
    {        
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _collider = GetComponent<Collider>();
        drone = transform.root.GetComponent<Drone>();       
        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
        _perfectWait = new WaitForSeconds(maxPerfectTime);
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

        StartCoroutine(PerfectTimer());
    }

    private IEnumerator PerfectTimer()
    {
        yield return _perfectWait;
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
        leftHandGrabInteractor.ForceRelease();
        rightHandGrabInteractor.ForceRelease();
        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
        transform.parent = drone.attachPoint.transform;
        transform.localPosition = Vector3.zero;
        isPerfect = true;      
    }

    public void Miss()
    {
        drone.gameManager.OnBurgerMissed();
    }
}