using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToucanOpenMouth : MonoBehaviour {
    public AudioClip birdCall1;
    public AudioClip birdCall2;
    public AudioSource audioSource;
    Animator animator;

    void Start () {
        animator = transform.GetComponentInChildren<Animator>();
        audioSource.clip = birdCall1;
        audioSource.clip = birdCall2;

        if (animator == null)
        {
            Debug.LogError("Didn't find animator!");
        }
        StartCoroutine(openMouth());
    }

	void Update ()
    {


    }
    
    IEnumerator openMouth()
    {
        float max = 10.0f;
        float waitTime = Random.Range(0.0f, max);
        animator.SetTrigger("openMouth");

        if (waitTime <= (max / 2))
        {
            audioSource.PlayOneShot(birdCall1);
        }
        else if (waitTime >= (max / 2))
        {
            audioSource.PlayOneShot(birdCall2);
        }

        yield return new WaitForSeconds(waitTime);
        StartCoroutine(openMouth());

    }
}
