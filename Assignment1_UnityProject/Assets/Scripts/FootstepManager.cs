using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    [System.Serializable]
    public class SurfaceSound
    {
        public string surfaceTag;
        public List<AudioClip> footstepSounds;
    }

    public LayerMask groundLayer;
    public float raycastDistance = 0.5f;

    [Header("Surface-Specific Sounds")]
    public List<SurfaceSound> surfaceSounds = new List<SurfaceSound>();

    [Header("Default/Fallback Sounds")]
    public List<AudioClip> defaultFootstepClips;

    public AudioSource mAudioSource;

    private RaycastHit hit;
    private string currentGroundTag = "";
    private Dictionary<string, List<AudioClip>> soundDictionary;

    private void Start()
    {
        // Build dictionary for faster lookups
        soundDictionary = new Dictionary<string, List<AudioClip>>();
        foreach (var surfaceSound in surfaceSounds)
        {
            if (!string.IsNullOrEmpty(surfaceSound.surfaceTag))
            {
                soundDictionary[surfaceSound.surfaceTag] = surfaceSound.footstepSounds;
            }
        }
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    void CheckGround()
    {
        Debug.DrawRay(transform.position, Vector3.down * raycastDistance, Color.red, 0.1f);

        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, groundLayer))
        {
            Debug.Log("Hit: " + hit.collider.name + " Tag: " + hit.collider.tag);

            string newGroundTag = hit.collider.tag;
            if (newGroundTag != currentGroundTag)
            {
                currentGroundTag = newGroundTag;
            }
        }
        else
        {
            Debug.Log("Raycast missed - no ground detected");
            currentGroundTag = "";
        }
    }

    public void PlayFootstepSound()
    {
        if (string.IsNullOrEmpty(currentGroundTag))
        {
            return;
        }

        List<AudioClip> clips = null;

        // Try to get sounds for the specific surface tag
        if (soundDictionary.TryGetValue(currentGroundTag, out clips) && clips != null && clips.Count > 0)
        {
            // Play random clip from the list for variety
            AudioClip soundToPlay = clips[Random.Range(0, clips.Count)];
            mAudioSource.PlayOneShot(soundToPlay);
        }
        // Fall back to default sounds if no match found
        else if (defaultFootstepClips != null && defaultFootstepClips.Count > 0)
        {
            AudioClip soundToPlay = defaultFootstepClips[Random.Range(0, defaultFootstepClips.Count)];
            mAudioSource.PlayOneShot(soundToPlay);
        }
    }
}