using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Runtime.Core.Herbalist
{
    public class PlayerData
    {
        private List<string> _storyMarks = new List<string>();

        public List<string> StoryMarks => _storyMarks;

        public void AddStoryMarks(string[] marks)
        {
            _storyMarks.AddRange(marks);
        }

        public void RemoveStoryMarks(string[] marks)
        {
            foreach (var mark in marks)
            {
                _storyMarks.Remove(mark);
            }
        }

        public bool HasAtLeastOneMark(string[] marks)
        {
            return marks.Any((mark) => _storyMarks.Contains(mark));
        }

        public bool HasStoryMarks(string[] marks)
        {
            return marks.All((mark)=>_storyMarks.Contains(mark));
        }
    }
}