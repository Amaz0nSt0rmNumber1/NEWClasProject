using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [Header("Game Variables")]
    public PlayerController Moon;
    public float time;
    public bool timeActive;

    [Header("Countdown")]
    public int countdown;

    [Header("Game UI")]
    public TMP_Text gameUI_score;
    public TMP_Text gameUI_health;
    public TMP_Text game_UI_time;

    [Header("Countdown UI")]
    public TMP_Text countdownText;

    [Header("End Screen UI")]
    public TMP_Text endUI_score;
    public TMP_Text endUI_time;

    [Header("Screens")]
    public GameObject countdownUI;
    public GameObject gameUI;
    public GameObject endUI;

    // Start is called before the first frame update
    void Start()
    {
        Moon = GameObject.Find("Moon").GetComponent<PlayerController>();

        time = 0;

        Moon.enabled = false;

        //set the screen to show the countdown
        SetScreen(countdownUI);

        StartCoroutine(CountDownRoutine());

    }

    IEnumerator CountDownRoutine()
    {
        countdownText.gameObject.SetActive(true);

        countdown = 3;
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;

        }
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        Moon.enabled = true;

        startGame();
    }

    void startGame()
    {
        SetScreen(gameUI);
        //start the timer 
        timeActive = true;
    }

    public void endGame()
    {
        // end the timer 
        timeActive = false;

        // disable player movement
        Moon.enabled = false;

        //set the UI to display your stats
        endUI_score.text = "Score: " + Moon.coinCount;
        endUI_time.text = "Time: " + (time * 10).ToString("F2");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SetScreen(endUI);
    }


    // Update is called once per frame
    void Update()
    {
        //keep track of the time that goes by
        if (timeActive)
        {
            time = time + Time.deltaTime;
        }
        gameUI_score.text = "Coins:" + Moon.coinCount;
        gameUI_health.text = "Health:" + Moon.health;
        game_UI_time.text = "Time:" + (time * 10).ToString("F2");
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene(0);
    }
    public void SetScreen(GameObject screen)
    {
        //disable all other screens
        countdownUI.SetActive(false);
        gameUI.SetActive(false);
        endUI.SetActive(false);
       
        //activate the requested screen 
        screen.SetActive(true);
    }

}

