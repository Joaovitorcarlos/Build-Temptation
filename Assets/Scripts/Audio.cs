using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource music;
    public double startDelay = 0.5;

    void Start()
    {
        double startTime = AudioSettings.dspTime + startDelay;
        music.PlayScheduled(startTime);
    }
}