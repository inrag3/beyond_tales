using System.Collections.Generic;
using UnityEngine;
using _Project.Runtime.Core.Herbalist;

internal interface IScanner
{
    List<T> Scan<T>(Vector3 at, float radius) where T : ITransformable; 
}
