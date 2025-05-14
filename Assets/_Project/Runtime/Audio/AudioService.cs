using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Audio
{
    public class AudioService : MonoBehaviour, IAudioService
    {
        [SerializeField]private SoundSettings _soundSettings;
        [SerializeField] private GameObject _audioSourcePrefab;
        [SerializeField] private int _initialPoolSize;
        [SerializeField] private float _timeToMusicTransfer;
        private List<AudioSource> _audioSourcePool = new List<AudioSource>();

        private AudioSource _musicSource;
        private AudioSource _musicTransfer;
        private AudioSource _walkSource;

        private IEnumerator routineCash;
        private void Awake()
        {
            for (int i = 0; i < _initialPoolSize; i++)
            {
                AddNewAudioSourceToPool();
            }
            
            _musicSource = Instantiate(_audioSourcePrefab, transform).GetComponent<AudioSource>();
            InitializeAudioSource(_musicSource);
            _musicSource.loop = true;
            
            _musicTransfer = Instantiate(_audioSourcePrefab, transform).GetComponent<AudioSource>();
            InitializeAudioSource(_musicTransfer);
            _musicTransfer.loop = true;
            
            _walkSource = Instantiate(_audioSourcePrefab, transform).GetComponent<AudioSource>();
            InitializeAudioSource(_walkSource);
        }

        public void PlayWalkSound()
        {
            if (_soundSettings.walkClip == null) return;
            if (_walkSource.isPlaying && _walkSource.clip == _soundSettings.walkClip)
                return;

            _walkSource.clip = _soundSettings.walkClip;
            _walkSource.loop = true;
            _walkSource.Play();
        }

        public void StopWalkSound()
        {
           _walkSource.Stop();
        }

        public void PlayOneShot(AudioClip clip)
        {
            if(clip == null) return;
            
            GetFreeAudioSource().PlayOneShot(clip);
        }

        private AudioSource AddNewAudioSourceToPool()
        {
            var source = Instantiate(_audioSourcePrefab, transform).GetComponent<AudioSource>();
            InitializeAudioSource(source);
            _audioSourcePool.Add(source);
            return source;
        }

        private void InitializeAudioSource(AudioSource audioSource)
        {
            audioSource.spatialBlend = 0f;
            audioSource.volume = 1f;
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }

        public AudioSource GetFreeAudioSource()
        {
            foreach (var source in _audioSourcePool)
            {
                if (!source.isPlaying)
                {
                    return source;
                }
            }

            return AddNewAudioSourceToPool();
        }

        public void ChangeMusic(AudioClip clip)
        {
            if (_musicSource.isPlaying)
            {
                if (_musicSource.clip == clip)
                {
                    return;
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(ChangeMusicCoroutine(clip, false));
                }
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(ChangeMusicCoroutine(clip, true));
            }
            
            
        }

        private IEnumerator ChangeMusicCoroutine(AudioClip newMusic, bool transferFromNone)
        {
            _musicSource.volume = 0;
            _musicTransfer.volume = _musicSource.volume;
            _musicTransfer.clip = _musicSource.clip;
            _musicSource.clip = newMusic;

            _musicSource.Play();
            _musicTransfer.Play();

            float transferSpeed = 1 / _timeToMusicTransfer;
            while (_musicSource.volume < 1)
            {
                _musicSource.volume += Time.deltaTime * transferSpeed;
                if (_musicSource.volume > 1)
                {
                    _musicSource.volume = 1;
                }

                _musicTransfer.volume -= Time.deltaTime * transferSpeed;

                if (_musicTransfer.volume < 0 || transferFromNone)
                {
                    _musicTransfer.volume = 0;
                }

                yield return null;
            }
            _musicTransfer.Stop();
        }
    }
}
