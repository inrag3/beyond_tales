using System.Linq;
using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.SaveSystem
{
    public class GameLoader
    {
        private SaveLoader _saveLoader;
        private IHerbalistProvider _herbalistProvider;
        private DontDestroyContainer _dontDestroyContainer;
        
        [Inject]
        private void Construct(SaveLoader saveLoader,
            IHerbalistProvider herbalistProvider, Door[] doors, ISceneManager sceneManager, DontDestroyContainer dontDestroyContainer)
        {
            _saveLoader = saveLoader;
            _herbalistProvider = herbalistProvider;
            _dontDestroyContainer = dontDestroyContainer;
            _dontDestroyContainer.OnActivateScene += OnLoadScene;
        }
        public void LoadGame()
        {
            var _doors = GameObject.FindObjectsOfType<Door>();
            
            var save = _saveLoader.Save;
            _herbalistProvider.Herbalist.Transform.position = save.playerPos.UnityVector;
            _herbalistProvider.Herbalist.PlayerData.AddStoryMarks(save.storyMarks.ToArray());
            
            var sortedDoors = _doors.OrderBy((d) => d.Transform.position.x)
                .ThenBy((d) => d.Transform.position.z);

            int i = 0;
            foreach (var door in sortedDoors)
            {
                Debug.Log($"Load door at pos {door.Transform.position}, lock = {save.doors[i].isLocked}, open = {save.doors[i].isOpen}");
                door.Lock(save.doors[i].isLocked);
                if (door.IsOpen)
                {
                    if (!save.doors[i].isOpen)
                    {
                        door.Close();
                    }
                }
                else
                {
                    if (save.doors[i].isOpen)
                    {
                        door.Open();
                    }
                }

                ++i;
            }

            var storyMarksHandlers = GameObject.FindObjectsOfType<DisableObjectIfHasStoryMark>();
            foreach (var handler in storyMarksHandlers)
            {
                if (_herbalistProvider.Herbalist.PlayerData.HasStoryMarks(new string[]{handler.StoryMark}))
                {
                    handler.Activate();
                }
            }

            if (save.plantPuzzleSolved)
            {
                GameObject.FindObjectOfType<BedsObserver>().CompletePuzzle();
            }
        }

        private void OnLoadScene(Scene scene)
        {
            if (scene == Scene.MainCopyTestScreenplay)
            {
                if (_dontDestroyContainer.RequireSaveLoad)
                {
                    _dontDestroyContainer.RequireSaveLoad = false;
                    LoadGame();
                }
            }
        }
    }
}