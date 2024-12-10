using System.Collections.Generic;
using System.Linq;
using _Project.Runtime.Core.Interactables;
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
            Container.BindInterfacesAndSelfTo<Dialoguer>().AsSingle().NonLazy();
            Container.Bind<DialogueManager>().FromInstance(_dialogueManager).AsSingle();
            Container.Bind<List<DialogueTrigger>>().FromMethod(_ => FindObjectsOfType<DialogueTrigger>().ToList()).AsSingle();
        }
    }
}