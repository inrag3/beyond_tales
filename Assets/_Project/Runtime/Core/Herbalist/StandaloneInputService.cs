using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Herbalist
{
    public class StandaloneInputService : IInputService, ITickable, IInitializable, IGizmoDrawer
    {
        private UnityEngine.Camera _camera;

        public StandaloneInputService(GizmosDrawer drawer)
        {
            drawer.Register(this);
        }
        
        public float Horizontal => Input.GetAxisRaw("Horizontal");
        public float Vertical => Input.GetAxisRaw("Vertical");
        public bool IsRollButtonPressed => Input.GetKeyDown(KeyCode.Space);
        public bool IsGrenadeButtonPressed => Input.GetKeyDown(KeyCode.Q);
        public bool IsDialogButtonPressed => Input.GetKeyDown(KeyCode.Tab);
        public bool IsAttackButtonPressed => Input.GetMouseButtonDown(0);
        public bool IsInteractButtonPressed => Input.GetKeyDown(KeyCode.E);
        
        public Vector3 Mouse { get; private set; }
        
        public void Initialize()
        {
            _camera = UnityEngine.Camera.main;
        }
        public void Tick()
        {
            var plane = new Plane(Vector3.up, Vector3.zero); // Y = 0, плоскость игрового поля
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (!plane.Raycast(ray, out float enter))
                return;
            Mouse = ray.GetPoint(enter);
            Mouse.Normalize();
        }

        public void Draw()
        {

            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(Mouse, 0.25f);
        }
    }
}