using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {
    public int p1Score;
    public int p2Score;
    public int scoreToWin;
    public int restartTime = 5;

    public Text p1Scoreboard;
    public Text p2Scoreboard;

    public Text p1GameOver;
    public Text p2GameOver;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        p1Scoreboard.text = "P1: " + p1Score + " " +  "P2: " + p2Score;
        p2Scoreboard.text = "P1: " + p1Score + " " + "P2: " + p2Score;

        if (p1Score == scoreToWin)
        {
            Victory(1);
        }

        if (p2Score == scoreToWin)
        {
            Victory(2);
        }
	}

    public void AddScore(int playerNumber)
    {
        if (playerNumber == 1)
        {
            p1Score++;
            Debug.Log(p1Score);
        }

        if (playerNumber == 2)
        {
            p2Score++;
        }
    }

    public void Victory(int playerNumber)
    {
        if (playerNumber == 1)
        {
            p1GameOver.text = "You Win";
            p2GameOver.text = "You Lose";
            StartCoroutine(RestartMatch());
        }

        if (playerNumber == 2)
        {
            p1GameOver.text = "You Lose";
            p2GameOver.text = "You Win";
            StartCoroutine(RestartMatch());
        }
    }

    public IEnumerator RestartMatch()
    {
        yield return new WaitForSeconds(restartTime);

        SceneManager.LoadScene("scene0");


    }
}
