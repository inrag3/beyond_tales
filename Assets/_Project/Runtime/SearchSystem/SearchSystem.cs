using System.Collections.Generic;
using UnityEngine;

namespace _Project.Runtime.SearchSystem
{
    public class SearchSystem
    {
        private Dictionary<string, SearchIndex> _indices = new Dictionary<string, SearchIndex>();

        public void AddIndex(SearchIndex index)
        {
            if (_indices.ContainsKey(index.Index))
            {
                Debug.LogError($"Ошибка! Объект с индексом \"{index.Index}\" уже существует! " +
                               $"Придумайте другой индекс для объекта или удалите предыдущий если он не используется!");
                return;
            }

            _indices[index.Index] = index;
        }

        public SearchIndex GetIndex(string index)
        {
            if (_indices.TryGetValue(index, out var searchIndex))
            {
                return searchIndex;
            }
            else
            {
                Debug.LogError($"Ошибка не удалось найти объект с индексом \"{index}\"!");

                return null;
            }
        }
    }
}