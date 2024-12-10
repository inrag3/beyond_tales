using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Core.Interactables.Processors;
using Zenject;

namespace _Project.Runtime.Core.Herbalist
{
    public class Interactor : ITickable
    {
        private IInputService _inputService;
        private IScanner<Interactable> _scanner;
        private IPlayerInventory _playerInventory;
        private IInteractableVisitor _visitor;

        [Inject]
        private void Construct(
            IInputService inputService, 
            IScanner<Interactable> scanner,
            IInteractableVisitor visitor)
        {
            _visitor = visitor;
            _scanner = scanner;
            _inputService = inputService;
        }

        public void Tick()
        {
            if (!_inputService.IsInteractButtonPressed || _scanner.IsEmpty)
                return;
            
            var intractable = _scanner.Get();
            
            intractable.Interact(_visitor);
        }
    }
}
