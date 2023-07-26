using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.Oculus;

public class MouthController : MonoBehaviour
{
    public GameManager gameManager;
    private Burger _currentBurger;

    private bool _mouthOpen;
    public bool MouthOpen { get => _mouthOpen; set => _mouthOpen = value; }
    public bool hasBitten;
    
    public bool perfectBite;



    private void Start()
    {
        var headsetType = Utils.GetSystemHeadsetType();
        Debug.Log("System headset type: " + headsetType);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("burger"))
        {
            _currentBurger = other.GetComponent<Burger>();

            Debug.Log("Burger entered mouth");                     

            _currentBurger.Eat();
            
            gameManager.AddPoints(_currentBurger.isPerfect);            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("burger"))
        {
            if (other.gameObject == _currentBurger.gameObject)
                _currentBurger = null;
        }
    }

    private void Update()
    {
        //// mouth open, reset has bitten
        //if (_mouthOpen)
        //{
        //    hasBitten = false;
        //}
        //else // close mouth, take a bite
        //{
        //    if (!hasBitten)
        //    {
        //        hasBitten = true;

        //        if (_currentBurger != null)
        //        {
        //            gameManager.AddPoints((Vector3.Distance(transform.position, _currentBurger.transform.position) < perfectDistance));

        //            _currentBurger.Reset();
        //        }
        //    }
        //}
    }
}