using UnityEngine;

namespace _Project.Runtime.Audio
{
    public interface IAudioService
    {
        void PlayWalkSound();
        void StopWalkSound();

        public void PlayOneShot(AudioClip clip);
    }
}
