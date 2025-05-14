using System.Linq;
using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Infrastructure.Factories;
using Zenject;

namespace _Project.Runtime.Core.SaveSystem
{
    public class GameLoader
    {
        private SaveLoader _saveLoader;
        private IHerbalistProvider _herbalistProvider;
        private Door[] _doors;
        
        [Inject]
        private void Construct(SaveLoader saveLoader,
            IHerbalistProvider herbalistProvider, Door[] doors)
        {
            _saveLoader = saveLoader;
            _herbalistProvider = herbalistProvider;
            _doors = doors;
        }
        public void LoadGame()
        {
            var save = _saveLoader.Save;
            _herbalistProvider.Herbalist.Transform.position = save.playerPos;
            _herbalistProvider.Herbalist.PlayerData.AddStoryMarks(save.storyMarks.ToArray());
            
            var sortedDoors = _doors.OrderBy((d) => d.Transform.position.x)
                .ThenBy((d) => d.Transform.position.z);

            int i = 0;
            foreach (var door in sortedDoors)
            {
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
        }
    }
}