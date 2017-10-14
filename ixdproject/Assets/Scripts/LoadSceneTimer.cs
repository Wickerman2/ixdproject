using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadSceneTimer : MonoBehaviour
{

    float loadSceneTimer = 5.5f;
    private DetectPlayer detectPlayer;


    // Use this for initialization
    void Start()
    {
        detectPlayer = GameObject.Find("GameControl").GetComponent<DetectPlayer>();
        GetComponent<GUIText>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (detectPlayer.playerDetected == true)
        {
            GetComponent<GUIText>().enabled = true;
            loadSceneTimer -= Time.deltaTime;
            GetComponent<GUIText>().text = "" + Mathf.RoundToInt(loadSceneTimer);
            var timer = GetComponent<GUIText>().text;

            if (timer == "0")
            {
                SceneManager.LoadScene("GameScene", LoadSceneMode.Single);

            }
        }
        else if (detectPlayer.playerDetected == false)
        {
            GetComponent<GUIText>().enabled = false;
            loadSceneTimer = 5.5f;
        }
    }
}
