using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Rendering.Universal;
using UnityEngine.Timeline;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public int score;
    public Drone Drone1;
    public Drone Drone2;
    public VideoPlayer videoPlayer;
    public float gameTime; 

    public Camera playerCamera;
    public Color menuColour;
    public Color gameplayColour;

    public AudioSource music;
    public AudioSource sfx_bell;
    public AudioSource perfectSFX;

    public PlayableDirector instructionsTimeline;
    public PlayableDirector completeTimeLine;

    public GameObject poses;
    public GameObject poseMesh;

    public enum Gamestate
    {
        Menu,
        Gameplay,
    }

    public Gamestate gamestate;

    private void Start()
    {
        StartMenu();
        //StartGameplay();
    }

    public void StartInstructions()
    {
        instructionsTimeline.Play();
        music.time = 6f;
        music.Play();
    }

    public void StartGameplay()
    {
        Debug.Log("Game Started");
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.time = 0f;
        videoPlayer.Play();
        videoPlayer.GetComponent<Animator>().Play("PlayVideo");
        Drone1.gameObject.SetActive(true);
        Drone1.Fly();
        playerCamera.backgroundColor = gameplayColour;      
        StartCoroutine(GameTimer());
      
    }

    public void StartMenu()
    {
        playerCamera.backgroundColor = menuColour;
    }

    private IEnumerator GameTimer()
    {
       yield return new WaitForSeconds(gameTime);
       EndGame();
    }

    //end game after 1 minute   
    public void EndGame()
    {
        Debug.Log("Game Ended");
        Drone1.gameObject.SetActive(false);
        Drone2.gameObject.SetActive(false);

        music.Stop();
        sfx_bell.Play();

        videoPlayer.Stop();
        videoPlayer.gameObject.SetActive(false);
  

        completeTimeLine.Play();
        StartMenu();
    }

    public void ShowPose()
    {
        poses.SetActive(true);
        poseMesh.SetActive(true);
    }

    public void AddPoints(bool perfect)
    {
        // sound
        score += UnityEngine.Random.Range(500, 1000);
        if (perfect)
        {
            perfectSFX.Play();
            score += 1000;
        }
    }
}
