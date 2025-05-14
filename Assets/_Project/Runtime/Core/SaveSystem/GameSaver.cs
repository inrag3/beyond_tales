using System.Collections.Generic;
using System.Linq;
using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.SaveSystem
{
    public class GameSaver
    {
        private SaveLoader _saveLoader;
        private IHerbalistProvider _herbalistProvider;

        [Inject]
        private void Construct(SaveLoader saveLoader,
            IHerbalistProvider herbalistProvider, Door[] doors)
        {
            _saveLoader = saveLoader;
            _herbalistProvider = herbalistProvider;
        }
        
        public void SaveGame(Vector3 checkPointPos)
        {
            var _doors = GameObject.FindObjectsOfType<Door>();
            var sortedDoors = _doors.OrderBy((d) => d.Transform.position.x)
                .ThenBy((d) => d.Transform.position.z);
            List<GameSave.DoorSave> doorSaves = new List<GameSave.DoorSave>();

            foreach (var door in sortedDoors)
            {
                var doorSave = new GameSave.DoorSave();
                doorSave.isOpen = door.IsOpen;
                doorSave.isLocked = door.IsLocked;
                doorSaves.Add(doorSave);
            }

            _saveLoader.SaveGame(checkPointPos,
                _herbalistProvider.Herbalist.PlayerData.StoryMarks, doorSaves, GameObject.FindObjectOfType<BedsObserver>().PuzzleCompleted);
        }
    }
}