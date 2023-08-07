using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;
using Oculus.Interaction.HandGrab;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public bool testMode;

    public int score;
    private int _misses;
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

    public HandGrabInteractor leftHandGrabInteractor;
    public HandGrabInteractor rightHandGrabInteractor;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public Image bonusFillImage;
    public Image[] missImages;
    private Material _missMaterial;

    private void Start()
    {
        _missMaterial = missImages[0].material;
        Drone1.gameManager = this;
        Drone2.gameManager = this;

        if (testMode)
            StartGameplay();
        else
            StartMenu();
    }

    public void StartInstructions()
    {
        instructionsTimeline.Play();
        music.time = 6f;
        music.Play();
    }

    public void StartGameplay()
    {
        score = 0;
        _misses = 0;
        bonusFillImage.fillAmount = 0f;
        scoreText.text = "Score: " + score.ToString();

        for (int i = 0; i < missImages.Length; i++)
        {
            missImages[i].material = _missMaterial;
        }

        videoPlayer.gameObject.SetActive(true);
        videoPlayer.time = 0f;
        videoPlayer.Play();
        videoPlayer.GetComponent<Animator>().Play("PlayVideo");
        playerCamera.backgroundColor = gameplayColour;

        Drone1.gameObject.SetActive(true);
        Drone1.Fly();
        
        StartCoroutine(GameTimer());      
    }

    public void StartMenu()
    {
        playerCamera.backgroundColor = menuColour;
    }

    private IEnumerator GameTimer()
    {
        float duration = gameTime;

        while (duration > 0)
        {
            duration -= Time.deltaTime;
            timerText.text =  duration.ToString("F2");
            yield return null;
        }

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
        if (perfect)
        {
            perfectSFX.Play();
            score += 1000;
            
            bonusFillImage.fillAmount += Random.Range(0.2f, 0.35f);            
        }
        else
            score += UnityEngine.Random.Range(300, 700);

        scoreText.text = score.ToString();
    }
    
    public void OnBurgerMissed()
    {
        _misses++;

        score -= 500;

        if (_misses > 3)
            return;

        for (int i = 0; i < _misses; i++)
        {
            missImages[i].material = null;
        }
    }
}