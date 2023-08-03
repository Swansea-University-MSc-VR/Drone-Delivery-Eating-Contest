using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.Oculus;
using System.Linq;

public class MouthController : MonoBehaviour
{
    public GameManager gameManager;
    private Burger _currentBurger;

    private bool _mouthOpen;
    private bool _hasBitten;
    private float _jawOpenPercentage;

    public OVRFaceExpressions ovrFaceExpressions;

    public bool hasFaceTracking;

    private void Start()
    {
        var headsetType = Utils.GetSystemHeadsetType();
        Debug.Log("System headset type: " + headsetType);
        string headsetString = headsetType.ToString();

        if (headsetString == "Meta_Link_Quest_Pro" || headsetString == "Meta_Quest_Pro")
            hasFaceTracking = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("burger"))
        {
            _currentBurger = other.GetComponent<Burger>();
            Debug.Log("Burger entered mouth");

            if (!hasFaceTracking)
            {
                _currentBurger.Eat();
                gameManager.AddPoints(_currentBurger.isPerfect);
            }
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
        if (!hasFaceTracking)
            return;

        if (ovrFaceExpressions.ValidExpressions)
            _jawOpenPercentage = ovrFaceExpressions.GetWeight(OVRFaceExpressions.FaceExpression.JawDrop) * 100f;

        if (_jawOpenPercentage > 40f)
        {
            _mouthOpen = true;
            _hasBitten = false;
        }
        else
        {
            _mouthOpen = false;

            if (!_hasBitten)
            {
                _hasBitten = true;

                if (_currentBurger == null)
                    return;

                if (Vector3.Distance(_currentBurger.transform.position, transform.position) < 0.5f)
                {
                    gameManager.AddPoints(_currentBurger.isPerfect);
                    _currentBurger.Reset();
                    _currentBurger = null;
                }
            }
        }   
    }
}