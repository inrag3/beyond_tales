using System;
using System.Collections.Generic;
using UnityEngine;
using _Project.Runtime.Core.Herbalist;

public static class Extensions
{
    public static T Closest<T>(this IEnumerable<T> items, Vector3 origin) where T : ITransformable
    {
        T closest = default;
        float minDistance = float.MaxValue;

        foreach (var item in items)
        {
            float distance = Vector3.Distance(origin, item.Transform.position);
            if (distance < minDistance)
            {
                closest = item;
                minDistance = distance;
            }
        }

        return closest;
    }
    
    
    public static T Closest<T>(this IEnumerable<T> items, Vector3 origin, Predicate<T> predicate) where T : ITransformable
    {
        T closest = default;
        float minDistance = float.MaxValue;

        foreach (var item in items)
        {
            float distance = Vector3.Distance(origin, item.Transform.position);
            if (distance < minDistance && predicate(item))
            {
                closest = item;
                minDistance = distance;
            }
        }

        return closest;
    }
    
    
}
