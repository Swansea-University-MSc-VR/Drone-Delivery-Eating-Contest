using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetWall : MonoBehaviour
{
     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("burger"))
        {
            other.GetComponent<Burger>().Reset();
        }
    }
}
