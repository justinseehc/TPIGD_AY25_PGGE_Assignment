using UnityEngine;
using System.Collections.Generic;

public class FootstepManager : MonoBehaviour
{
    public AudioClip defaultSound;
    public List<GroundAudioPair> groundAudioPairs;
    private AudioSource audioSource;
    private float raycastHeight = 0.5f; // Adjust based on character height
    private float raycastDistance = 0.7f; // Must be greater than height to hit ground

    [System.Serializable]
    public class GroundAudioPair
    {
        public string groundTag;
        public AudioClip sound;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on character!");
        }
    }

    // Call this method via an Animation Event at the moment a foot hits the ground
    public void PlayFootstepSound()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * raycastHeight;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, raycastDistance))
        {
            AudioClip clipToPlay = defaultSound;

            foreach (var pair in groundAudioPairs)
            {
                if (hit.collider.CompareTag(pair.groundTag))
                {
                    clipToPlay = pair.sound;
                    break;
                }
            }

            if (clipToPlay != null)
            {
                audioSource.PlayOneShot(clipToPlay);
            }
        }
    }
}
