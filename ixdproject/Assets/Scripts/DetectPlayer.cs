using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class DetectPlayer : MonoBehaviour
{
    public GameObject BodySrcManager;
    private BodySourceManager bodyManager;

    private Body[] bodies;
    public Body body;
    public bool playerDetected = false;
    private ulong currTrackingId = 0;
    public float playerLength;

    void Start()
    {
        if (BodySrcManager == null)
        {
            Debug.Log("BodySourceManager is null! Assign a bodysrcManager ");
        }
        else
        {
            bodyManager = BodySrcManager.GetComponent<BodySourceManager>();
        }
        Debug.Log("PlayerLength 1: " + playerLength);

        GameObject.Find("PlayerDetectedGUI").GetComponent<GUIText>().enabled = false;
    }

    void Update()
    {
        body = GetActiveBody();

        if (body != null)
        {
            //trackBody(body);
        }
        Debug.Log("Player Detected: " + playerDetected);

        if (playerDetected == true)
        {
            GameObject.Find("PlayerDetectedGUI").GetComponent<GUIText>().enabled = true;
        }
        else if (playerDetected == false)
        {
            GameObject.Find("PlayerDetectedGUI").GetComponent<GUIText>().enabled = false;
        }
    }

    private Body GetActiveBody()
    {
        if (bodyManager == null)
        {
            Debug.Log("Body manager is null!");
        }
        bodies = bodyManager.GetData();

        if (currTrackingId <= 0)
        {
            foreach (Body body in bodies)
            {
                if (body.IsTracked)
                {
                    float zMeters = body.Joints[JointType.SpineBase].Position.Z;
                    float xMeters = body.Joints[JointType.SpineBase].Position.X;
                    float head = body.Joints[JointType.Head].Position.Y;
                    float left_foot = body.Joints[JointType.FootLeft].Position.Y;
                    float right_foot = body.Joints[JointType.FootRight].Position.Y;

                    playerLength = head - ((left_foot + right_foot) / 2);
                    PlayerPrefs.SetFloat("PlayerLength", playerLength);

                    Debug.Log("PlayerLength 2 : " + playerLength);

                    if (zMeters < 2.5f && zMeters > 1.5f && xMeters < 0.2f && xMeters > -0.2f)
                    {
                        currTrackingId = body.TrackingId;
                        playerDetected = true;
                        return body;
                    }
                }
                else if (!body.IsTracked)
                {
                    playerDetected = false;
                }
            }
            return null;
        }
        else
        {
            foreach (Body body in bodies)
            {
                float zMeters = body.Joints[JointType.SpineBase].Position.Z;
                float xMeters = body.Joints[JointType.SpineBase].Position.X;

                if (body.IsTracked && body.TrackingId == currTrackingId && zMeters < 2.5f && zMeters > 1.5f && xMeters < 0.2f && xMeters > -0.2f)
                {
                    playerDetected = true;

                    return body;
                }
            }
        }
        currTrackingId = 0;
        return GetActiveBody();
    }
}
