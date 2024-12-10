using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Infrastructure.Installers.SceneInstallers
{
    public class DialogueSystemInstaller : MonoInstaller
    {
        [SerializeField] private List<DialogueTrigger> _triggers;
        
        [SerializeField] private DialogueManager _dialogueManager;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Dialoguer>().AsSingle();
            Container.Bind<DialogueManager>().FromInstance(_dialogueManager).AsSingle();
            //Как-то забиндить List<DialogueTrigger>
        }
    }
}