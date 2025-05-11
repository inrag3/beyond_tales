using UnityEngine;
using Zenject;
using _Project.Runtime.Audio;

namespace _Project.Runtime.Audio
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAudioController : MonoBehaviour
    {
        IAudioService _audioService;

        bool _isWalking;
        bool _isAttacking;

        [Inject]
        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }

        private void Update()
        {
            bool walkingKeys =
                Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
                Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
                Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);

            if (walkingKeys && !_isWalking)
            {
                _audioService.PlayWalkSound();
                _isWalking = true;
            }
            else if (!walkingKeys && _isWalking)
            {
                _audioService.StopWalkSound();
                _isWalking = false;
            }

            bool attackKey = Input.GetMouseButton(0);

            /*if (attackKey && !_isAttacking)
            {
                _audioService.PlayFightSound();
                _isAttacking = true;
            }
            else if (!attackKey && _isAttacking)
            {
                _audioService.StopFightSound();
                _isAttacking = false;
            }*/
        }
        public void PlayWalkSound() => _audioService.PlayWalkSound();
    }
}
