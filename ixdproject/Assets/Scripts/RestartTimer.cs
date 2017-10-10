using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartTimer : MonoBehaviour
{
    float restart_timer = 10.5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        restart_timer -= Time.deltaTime;
        GetComponent<GUIText>().text = "" + Mathf.RoundToInt(restart_timer);
        var timer = GetComponent<GUIText>().text;

        if (timer == "5")
        {
            GetComponent<GUIText>().enabled = true;
        }
        else if (timer == "0")
        {
            SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
        }
    }
}

