using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    public AudioSource mAudioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play(AudioClip clip, float volume = 1.0f, float pitch = 1.0f, bool loop = true)
    {
        if (mAudioSource.isPlaying)
            mAudioSource.Stop();
        mAudioSource.clip = clip;
        mAudioSource.pitch = pitch;
        mAudioSource.volume = volume;
        mAudioSource.loop = loop;
        mAudioSource.Play();
    }
}
