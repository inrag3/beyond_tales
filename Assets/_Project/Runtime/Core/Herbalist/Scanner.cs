using System.Collections.Generic;
using UnityEngine;
using BeyondTales.InventorySystem;
using _Project.Runtime.Core.Herbalist; 

public class Scanner : IScanner
{
    private readonly Collider[] _colliders = new Collider[10];

    public List<T> Scan<T>(Vector3 at, float radius) where T : ITransformable
    {
        var result = new List<T>(10);
        int size = Physics.OverlapSphereNonAlloc(at, radius, _colliders);

        for (var i = 0; i < size; i++)
        {
            Collider collider = _colliders[i];
            if (collider.TryGetComponent(out T item))
            {
                result.Add(item);
            }
        }

        return result;
    }
}
