using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.UI;

namespace Member.Choijeongyun._01.Scripts.Func
{
    public class CJY_AudioManager : MonoBehaviour
    {
        [SerializeField] private float volume;
        [SerializeField] AudioClip[] audioClips;
        private AudioSource[] _sources;
        private AudioSource _loopAudio;
        private AudioSource _audioSource;

        private void Awake()
        {
            _sources = GetComponents<AudioSource>(); // 복수형!
            _audioSource = _sources[0];
            _loopAudio = _sources[1];
            _audioSource.volume = volume;
            _loopAudio.volume = volume;
            _loopAudio.loop = true;
        }

        public void PlaySFX(int index, float delay)
        {
            if (index < 0 || index >= audioClips.Length) return;
            StartCoroutine(PlayDelayedSFX(index, delay));
        }

        private IEnumerator PlayDelayedSFX(int index, float delay)
        {
            yield return new WaitForSeconds(delay);
            _audioSource.PlayOneShot(audioClips[index]);
        }

        public void PlayLoop(int index)
        {
            if (_loopAudio.isPlaying) return;
            _loopAudio.clip = audioClips[index];
            
            _loopAudio.Play();
        }

        public void StopLoop()
        {
            _loopAudio.Stop();
        }
        
    }
}
