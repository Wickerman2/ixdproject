using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO.Ports;






public class GameTime : MonoBehaviour
{
    private DetectJoints DJ;
    private Score Score;
    public Animator Anim;
    public Image Img;
    public float gameTime = 30.0f;
    public SerialPort myData = new SerialPort("COM6", 19200);
    private DetectPlayer detectPlayer;

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
                StartCoroutine(Fade("LoseScene"));
            }
            else if (Score.score >= 3)
            {
                StartCoroutine(Fade("WinScene"));
                SendToArduino();
            }

        }

    }

    public void SendToArduino()
    {
        myData.Open();
        myData.WriteLine("1");
        myData.Close();
    }

    IEnumerator Fade(string sceneName)
    {
        Debug.Log(sceneName);
        Anim.SetBool("Fade", true);
        yield return new WaitUntil(() => Img.color.a == 1);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}

