using System.Collections.Generic;
using _Project.Runtime.Core.Herbalist;
using UnityEngine;

namespace _Project.Runtime.Core
{
    public class GizmosDrawer : MonoBehaviour
    {
        private readonly List<IGizmoDrawer> _drawers = new();
        public void Register(IGizmoDrawer drawer)
        {
            _drawers.Add(drawer);
        }
        
        private void OnDrawGizmos()
        {
            _drawers.ForEach(drawer => drawer.Draw());
        }
    }
}