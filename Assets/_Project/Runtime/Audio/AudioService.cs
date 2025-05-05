using UnityEngine;
using Zenject;

namespace _Project.Runtime.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioService : MonoBehaviour, IAudioService
    {
        [Inject] private SoundSettings _soundSettings;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.spatialBlend = 0f;
            _audioSource.volume = 1f;
            _audioSource.playOnAwake = false;
            _audioSource.loop = false;
        }

        public void PlayWalkSound()
        {
            if (_soundSettings.walkClip == null) return;
            if (_audioSource.isPlaying && _audioSource.clip == _soundSettings.walkClip)
                return;

            _audioSource.clip = _soundSettings.walkClip;
            _audioSource.loop = true;
            _audioSource.Play();
        }

        public void StopWalkSound()
        {
            if (_audioSource.clip == _soundSettings.walkClip)
                _audioSource.Stop();
        }

        public void PlayFightSound()
        {
            if (_soundSettings.fightClip == null) return;
            if (_audioSource.isPlaying && _audioSource.clip == _soundSettings.fightClip)
                return;

            _audioSource.clip = _soundSettings.fightClip;
            _audioSource.loop = true;
            _audioSource.Play();
        }

        public void StopFightSound()
        {
            if (_audioSource.clip == _soundSettings.fightClip)
                _audioSource.Stop();
        }
    }
}
