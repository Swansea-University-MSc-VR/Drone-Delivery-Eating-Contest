using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerMissDetection : MonoBehaviour
{  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("burger"))
        {
            other.gameObject.GetComponent<Burger>().Miss();
        }
    }
}