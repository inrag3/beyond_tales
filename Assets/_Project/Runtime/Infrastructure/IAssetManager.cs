using UnityEngine;

namespace _Project.Runtime.Infrastructure
{
    public interface IAssetManager
    {
        public GameObject Get(string path);
    }
}