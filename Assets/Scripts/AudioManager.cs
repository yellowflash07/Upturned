using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public AudioSource audioSource;

    public AudioClip impactAudio,flipAudio,captureAudio,explosionAudio,fishEat,sinkAudio,levelCompleteAudio;


    FishEaten fishEaten;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        fishEaten = FindObjectOfType<FishEaten>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TriggerScript.impact)
        {
            TriggerScript.impact = false;
            audioSource.clip = impactAudio;
            audioSource.Play();
        }

        if (PlayerController.upsidedown)
        {
            audioSource.clip = flipAudio;
            audioSource.Play();
        }

        if (TriggerScript.fishcap)
        {
            TriggerScript.fishcap = false;
            audioSource.clip = captureAudio;
            audioSource.Play();
        }

        if (TriggerScript.exploded)
        {
            TriggerScript.exploded = false;
            audioSource.clip = explosionAudio;
            audioSource.Play();
        }

        if (TriggerScript.fishEat)
        {
            TriggerScript.fishEat = false;
            audioSource.clip = fishEat;
            audioSource.Play();
        }

        if (TriggerScript.sink)
        {
            TriggerScript.sink = false;
            audioSource.clip = sinkAudio;
            audioSource.Play();
        }

        if (TriggerScript.levelComp)
        {
            TriggerScript.levelComp = false;
            audioSource.clip = levelCompleteAudio;
            audioSource.Play();            
        }
    }
}
