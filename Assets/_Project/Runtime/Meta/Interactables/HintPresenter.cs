using System;
using System.Collections.Specialized;
using _Project.Runtime.Core.Herbalist;
using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Infrastructure.Factories;
using _Project.Runtime.Infrastructure.Factories.UI;
using _Project.Runtime.Infrastructure.Installers.SceneInstallers;
using ObservableCollections;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Meta.Interactables
{
    public class HintPresenter : IInitializable, IDisposable
    {
        private readonly IIndicatorHandler _indicatorHandler;
        private readonly IHintViewFactory _hintViewFactory;
        private readonly Canvas _canvas;
        private readonly IScanner<Interactable> _scanner;
        private readonly IHerbalistProvider _provider;

        public HintPresenter(
            IHerbalistProvider provider,
            IIndicatorHandler indicatorHandler,
            IHintViewFactory hintViewFactory,
            Canvas canvas)
        {
            _provider = provider;
            _canvas = canvas;
            _hintViewFactory = hintViewFactory;
            _indicatorHandler = indicatorHandler;
        }

        public void Initialize()
        {
            _provider.Herbalist.Scanner.Components.CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(in NotifyCollectionChangedEventArgs<Interactable> e)
        {
            Interactable interactable;
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                interactable = e.NewItem;
                if (!interactable.IsAccessible)
                    return;
                IView view = _hintViewFactory.Create(_canvas.transform);
                _indicatorHandler.Register(interactable, view);
            }

            if (e.Action != NotifyCollectionChangedAction.Remove)
                return;
            
            interactable = e.OldItem;
            _indicatorHandler.Unregister(interactable);
        }

        public void Dispose()
        {
            _provider.Herbalist.Scanner.Components.CollectionChanged -= OnCollectionChanged;
        }
    }
}