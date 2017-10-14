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
                    if (zMeters < 2.0f && xMeters < 0.2f && xMeters > -0.2f)
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

                if (body.IsTracked && body.TrackingId == currTrackingId && zMeters < 2.0f && xMeters < 0.2f && xMeters > -0.2f)
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
