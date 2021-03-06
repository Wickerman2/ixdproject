﻿using UnityEngine;
using System.Collections;
using System.IO.Ports;
using UnityEngine.SceneManagement;



public class Score : MonoBehaviour
{

    public static int score = 0;
    public static SerialPort myData = new SerialPort("COM6", 19200); //Check if this matches with the Arduino code! 
    public Score instanceOfScore;
    static Score instance;
    BirdMovement bird;

    void Start()
    {
        int counter = 0;
        foreach (string str in SerialPort.GetPortNames()) //Checking if there is any Arduino connected to the computer.
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
    }

    static public void AddPoint()
    {
        score++;
        BirdMovement.forwardSpeed = BirdMovement.forwardSpeed + 1.0f;
    }

    void OnDestroy()
    {
        instance = null;
    }

    void Update()
    {
        GetComponent<GUIText>().text = "" + score;
    }
    public static void SendToArduino()
    {
        myData.Open();
        myData.WriteLine("1");
        myData.Close();
    }

}
