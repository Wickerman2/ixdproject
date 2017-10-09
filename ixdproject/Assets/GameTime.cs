using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;





public class GameTime : MonoBehaviour
{
    public DetectJoints DJ;
    float gameTime = 30.0f;


    private void Start()
    {
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

