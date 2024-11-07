using _Project.Runtime.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _smoothSpeed = 0.5f;
        private HerbalistFactory _herbalistFactory;
        private IHerbalistProvider _provider;

        [Inject]
        private void Construct(IHerbalistProvider provider)
        {
            _provider = provider;
        }

        private void LateUpdate()
        {
            Vector3 desiredPosition = _provider.Herbalist.Transform.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}