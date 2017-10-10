using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameControl : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneManager.LoadScene("LoseScene", LoadSceneMode.Single);
        }
    }
}
