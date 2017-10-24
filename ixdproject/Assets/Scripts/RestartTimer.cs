using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class RestartTimer : MonoBehaviour //A script with a timer to start over to the loading scene again! 
{
    float restart_timer = 10.5f;
    public Animator Anim;
    public Image Img;
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
            StartCoroutine(Fade("LoadingScene"));
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

