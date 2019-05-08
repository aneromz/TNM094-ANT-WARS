using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip SplatSound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        SplatSound = Resources.Load<AudioClip>("SplatSound");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound()
    {
        audioSrc.PlayOneShot(SplatSound);
    }


}
