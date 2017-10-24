using UnityEngine;
using System.Collections;

public class DisableOnStart : MonoBehaviour {

    // Use this for initialization
    void Start () //Letting the background music play through all scenes of the game! 
    {
        gameObject.SetActive (false);
    }
}
