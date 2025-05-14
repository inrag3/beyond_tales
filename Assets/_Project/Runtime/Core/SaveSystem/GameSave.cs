using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Runtime.Core.SaveSystem
{
    [Serializable]
    public class GameSave
    {
        public SerializableVector3 playerPos;
        public List<string> storyMarks;
        public List<DoorSave> doors;
        public bool plantPuzzleSolved;


        public void SetDefaultState()
        {
            storyMarks = new List<string>();
            doors = new List<DoorSave>();
        }
        
        public class DoorSave
        {
            public bool isOpen;
            public bool isLocked;
        }
    }
}