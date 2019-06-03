using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip SplatSound;
    public static AudioClip EggSound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        SplatSound = Resources.Load<AudioClip>("SplatSound");
        EggSound = Resources.Load<AudioClip>("egg_sound");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySplatSound()
    {
        audioSrc.PlayOneShot(SplatSound);
    }

    public static void PlayEggSound()
    {
        audioSrc.PlayOneShot(EggSound);
    }


}
