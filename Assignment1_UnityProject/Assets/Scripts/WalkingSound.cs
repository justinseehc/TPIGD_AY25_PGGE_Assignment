using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    public List<AudioClip> mAudioClip;
    public AudioSource mAudioSource;
    public int pos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound()
    {
        pos = (int)Mathf.Floor(Random.Range(0, mAudioClip.Count));
        mAudioSource.PlayOneShot(mAudioClip[pos]);
    }
}
