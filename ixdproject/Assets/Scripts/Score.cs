using UnityEngine;
using System.Collections;
using System.IO.Ports;
using UnityEngine.SceneManagement;



public class Score : MonoBehaviour
{

    public static int score = 0;
    static int highScore = 0;
    public SerialPort myData = new SerialPort("COM6", 19200);
    public Score instanceOfScore;
    static Score instance;





    BirdMovement bird;

    void Start()
    {

        int counter = 0;
        foreach (string str in SerialPort.GetPortNames())
        {
            Debug.Log(string.Format("Port :" + str));
            counter++;
        }
        Debug.Log("PortCounter: " + counter);

        instance = this;
        GameObject player_go = GameObject.FindGameObjectWithTag("Player");
        if (player_go == null)
        {
            Debug.LogError("Could not find an object with tag 'Player'.");
        }

        bird = player_go.GetComponent<BirdMovement>();
        score = 0;
        highScore = PlayerPrefs.GetInt("highScore", 0);
    }

    static public void AddPoint()
    {

        if (instance.bird.dead)
            return;

        score++;

        Score sc = new Score();
        //sc.SendToArduino();


        if (score > highScore)
        {
            highScore = score;
        }


            //Send info to machine
        
    }

    void OnDestroy()
    {
        instance = null;
        PlayerPrefs.SetInt("highScore", highScore);
    }

    void Update()
    {
        GetComponent<GUIText>().text = "" + score;
    }
    public void SendToArduino()
    {
        myData.Open();
        myData.WriteLine("1");
        myData.Close();
    }

}
