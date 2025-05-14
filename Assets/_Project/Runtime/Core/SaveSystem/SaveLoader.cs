using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Runtime.Core.SaveSystem
{
    public class SaveLoader
    {
        private const string _savePath = "/save.ear";
        private GameSave _save;

        public GameSave Save
        {
            get
            {
                if (_save == null)
                {
                    LoadSave();
                }
                return _save;
            }
        }

        private void LoadSave()
        {
            var path = Path.Combine(Application.persistentDataPath, _savePath);
            if (File.Exists(path))
            {
                _save = JsonConvert.DeserializeObject<GameSave>(File.ReadAllText(path));
            }
            else
            {
                _save = new GameSave();
                _save.SetDefaultState();
            }
        }

        public void SaveGame(Vector3 position, List<string> storyMarks, List<GameSave.DoorSave> saveDoors)
        {
            _save.playerPos = position;
            _save.storyMarks = storyMarks;
            _save.doors = saveDoors;
        }

        private void SaveGameInternal(GameSave save)
        {
            var path = Path.Combine(Application.persistentDataPath, _savePath);
            File.WriteAllText(path,JsonConvert.SerializeObject(save));
        }
    }
}