using UnityEngine;

public class Min_BossSound : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    public void Attack3Sound()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }
}
