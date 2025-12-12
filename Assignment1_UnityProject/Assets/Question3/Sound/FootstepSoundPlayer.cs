using UnityEngine;

public class FootstepSoundPlayer : MonoBehaviour
{
    public AudioClip[] ConcreteClips;
    public AudioClip[] DirtClips;
    public AudioClip[] MetalClips;
    public AudioClip[] WoodClips;

    public LayerMask Environment;

    public void playFootstepSound()
    {
        var clips = GetClipsForSurface();
        var randomClip = clips[Random.Range(0, clips.Length)];
        AudioSource.PlayClipAtPoint(randomClip, transform.position);
    }

    private AudioClip[] GetClipsForSurface()
    {
        var isHit = Physics.Raycast(transform.position + Vector3.up * .01f, Vector3.down, out var hit, 2f, Environment);
        if (isHit)
        {
            var surface = hit.collider.GetComponent<SurfaceDefinition>();
            if (surface)
            {
                if (surface.surfaceType == SurfaceType.Dirt) return DirtClips;
                if (surface.surfaceType == SurfaceType.Metal) return MetalClips;
                if (surface.surfaceType == SurfaceType.Wood) return WoodClips;
            }
        }
        return ConcreteClips;
    }
}
