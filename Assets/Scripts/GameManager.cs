using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int score;

    public void AddPoints(bool perfect)
    {
        // sound
        score += UnityEngine.Random.Range(500, 1000);
        if (perfect)
            score += 1000;

    }
}
