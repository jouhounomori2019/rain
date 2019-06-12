using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    public AudioClip audioClip3;
    private AudioSource audioSource;
    private AudioSource[] sources;

    void Start()
    {
        sources = gameObject.GetComponents<AudioSource>();
        //audioSource = gameObject.GetComponent<AudioSource>();
        //audioSource.clip = audioClip1;
        //audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            sources[0].Play();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            sources[0].Stop();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            sources[1].Play();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            sources[1].Stop();
        }
    }
}
