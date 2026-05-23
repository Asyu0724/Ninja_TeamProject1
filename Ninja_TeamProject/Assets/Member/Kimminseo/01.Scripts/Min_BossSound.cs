using System;
using UnityEngine;

public class Min_BossSound : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]private AudioClip[] audioClips;

    public enum Sfx {Attack3Knife, Attack2, Attack1And3};

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySfx(Sfx sfx)
    {
        AudioClip clip = audioSource.clip;
        clip = audioClips[(int)sfx];
        audioSource.PlayOneShot(clip);
    }
}
