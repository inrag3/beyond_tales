using System;
using System.Collections.Generic;
using _Project.Runtime.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.SearchSystem
{
    public class SearchIndex : MonoBehaviour
    {
        [SerializeField] private string _index;

        public string Index => _index;

        [SerializeField] private MonoBehaviour[] _indexedComponents;

        public MonoBehaviour[] IndexedComponents => _indexedComponents;

        private SearchSystem _searchSystem;
        [Inject]
        public void Construct(SearchSystem searchSystem)
        {
            _searchSystem = searchSystem;
            _searchSystem.AddIndex(this);
        }
    }
}