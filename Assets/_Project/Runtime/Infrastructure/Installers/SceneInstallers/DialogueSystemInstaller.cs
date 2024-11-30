using DialogueSystem;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Infrastructure.Installers.SceneInstallers
{
    public class DialogueSystemInstaller : MonoInstaller
    {
        [SerializeField] private DialogueManager _dialogueManager;

        public override void InstallBindings()
        {
            Container.Bind<DialogueManager>().FromInstance(_dialogueManager).AsSingle();
            Container.Bind<TestDialogueLauncher>().FromComponentInHierarchy().AsTransient();
        }
    }
}