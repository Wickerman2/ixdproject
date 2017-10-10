using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;





public class GameTime : MonoBehaviour
{
    private DetectJoints DJ;
    private Score Score;

    public float gameTime = 30.0f;


    private void Start()
    {
        Score = GameObject.Find("Score").GetComponent<Score>();
        DJ = GameObject.Find("PlayerBird").GetComponent<DetectJoints>();
    }
    // Update is called once per frame
    void Update()
    {
        var minutes = Mathf.RoundToInt(gameTime) / 60;
        var seconds = Mathf.RoundToInt(gameTime) % 60;

        if (DJ.GameStarted == true)
        {
            gameTime -= Time.deltaTime;
        }

        GetComponent<GUIText>().text = "" + string.Format("{0:0}:{1:00}", minutes, seconds);

        if (minutes == 0 && seconds == 0)
        {
            if (Score.score < 3)
            {
                SceneManager.LoadScene("LoseScene", LoadSceneMode.Single);
            }
            else if (Score.score >= 3)
            {
                SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
            }

        }
    }
}

