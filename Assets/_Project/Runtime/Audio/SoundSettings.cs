using UnityEngine;

namespace _Project.Runtime.Audio
{
    [CreateAssetMenu(menuName = "Audio/SoundSettings", fileName = "SoundSettings")]
    public class SoundSettings : ScriptableObject
    {
        [Header("Character Audio Clips")]
        public AudioClip walkClip;
        public AudioClip fightClip;
        public AudioClip solvePuzzleClip;
        public AudioClip battleMusic;
        public AudioClip peacefulMusic;
    }
}
