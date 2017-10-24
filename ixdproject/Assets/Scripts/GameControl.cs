using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{

    public Animator Anim;
    public Image Img;

    // Use this for initialization
    void Start()
    {
    }

    void Update() // Some code for debugging to reach all scenes and to activate the seed dispenser. 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Score.SendToArduino();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(Fade("GameScene"));
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

    IEnumerator Fade(string sceneName)
    {
        Anim.SetBool("Fade", true);
        yield return new WaitUntil(() => Img.color.a == 1);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
