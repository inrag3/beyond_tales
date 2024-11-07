using UnityEngine;

namespace _Project.Runtime.Core.Herbalist
{
    public class StandaloneInputService : IInputService
    {
        public float Horizontal => Input.GetAxisRaw("Horizontal");
        public float Vertical => Input.GetAxisRaw("Vertical");
        public bool IsRollButtonPressed => Input.GetKeyDown(KeyCode.Space);
    }
}