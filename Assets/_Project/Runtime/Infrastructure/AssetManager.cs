using System.Collections.Generic;
using UnityEngine;

namespace _Project.Runtime.Infrastructure
{
    public sealed class AssetManager : IAssetManager
    {
        private readonly Dictionary<string, GameObject> _prefabs = new();

        public GameObject Get(string path)
        {
            if (_prefabs.TryGetValue(path, out GameObject prefab))
                return prefab;

            var gameObject = Resources.Load<GameObject>(path);;
            _prefabs[path] = gameObject;
            return gameObject;
        }
    }
}