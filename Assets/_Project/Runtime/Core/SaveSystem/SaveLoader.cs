using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Runtime.Core.SaveSystem
{
    public class SaveLoader
    {
        private const string _savePath = "save.ear";
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

        public bool HasSave()
        {
            var path = Path.Combine(Application.persistentDataPath, _savePath);
            return File.Exists(path);
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

        public void SaveGame(Vector3 position, List<string> storyMarks, List<GameSave.DoorSave> saveDoors, bool plantPuzzleSolved)
        {
            var newSave = new GameSave();
            Debug.Log($"Save player pos {position}");
            newSave.playerPos = new SerializableVector3(position);
            newSave.storyMarks = storyMarks;
            newSave.doors = saveDoors;
            newSave.plantPuzzleSolved = plantPuzzleSolved;

            SaveGameInternal(newSave);
            _save = newSave;
        }

        private void SaveGameInternal(GameSave save)
        {
            var path = Path.Combine(Application.persistentDataPath, _savePath);
            Debug.Log($"try to save to {Application.persistentDataPath}");
            Debug.Log($"try to save to {path}");
            var settings = new JsonSerializerSettings();
            
            File.WriteAllText(path,JsonConvert.SerializeObject(save));
        }
    }
}