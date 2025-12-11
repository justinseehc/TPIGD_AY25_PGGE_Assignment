using UnityEngine;

public class FootstepSoundPlayer : MonoBehaviour
{
    public AudioClip[] GrassClips;
    public AudioClip[] StoneClips;
    public AudioClip[] WaterClips;

    public LayerMask Environment;

    public void playFootstepSound()
    {
        var isHit = Physics.Raycast(transform.position + Vector3.up * .01f, Vector3.down, out var hit, 1f, Environment);
        Debug.Log(isHit);
        if (isHit)
        {
            var surface = hit.collider.GetComponent<SurfaceDefinition>();
            if (surface)
            {

            }
            Debug.Log(surface.surfaceType);
        }
        else { }

    }
}
