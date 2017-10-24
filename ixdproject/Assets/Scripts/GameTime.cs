using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO.Ports;

public class GameTime : MonoBehaviour //This script controls the countdown in the GameScene. 
{
    private DetectFlap detectFlap;
    private Score Score;
    public Animator Anim;
    public Image Img;
    public float gameTime = 60.0f;
    private DetectPlayer detectPlayer;

    private void Start()
    {
        Score = GameObject.Find("Score").GetComponent<Score>();
        detectFlap = GameObject.Find("PlayerBird").GetComponent<DetectFlap>();
    }
    // Update is called once per frame
    void Update()
    {
        var minutes = Mathf.RoundToInt(gameTime) / 60;
        var seconds = Mathf.RoundToInt(gameTime) % 60;

        if (detectFlap.GameStarted == true)
        {
            gameTime -= Time.deltaTime;
        }

        GetComponent<GUIText>().text = "" + string.Format("{0:0}:{1:00}", minutes, seconds);

        if (minutes == 0 && seconds == 0)
        {
            if (Score.score < 3)
            {
                StartCoroutine(Fade("LoseScene"));
            }
            else if (Score.score >= 3) //If the score is above 3 points, send some information to the Arduino to let it provide the player with seeds. 
            {
                StartCoroutine(Fade("WinScene"));
                Score.SendToArduino();
            }

        }

    }


    IEnumerator Fade(string sceneName)
    {
        Debug.Log(sceneName);
        Anim.SetBool("Fade", true);
        yield return new WaitUntil(() => Img.color.a == 1);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}

