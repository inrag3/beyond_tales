using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using _Project.Runtime.Infrastructure.Factories;
using _Project.Runtime.Infrastructure.Factories.UI;
using ObservableCollections;
using Zenject;

namespace _Project.Runtime.Meta.Health
{
    public class EnemiesHealthPresenter : IInitializable, IDisposable
    {
        private readonly List<IEnemiesProvider> _enemiesProviders;
        private readonly IIndicatorHandler _indicatorHandler;
        private readonly IHealthViewFactory _healthViewFactory;

        public EnemiesHealthPresenter(List<IEnemiesProvider> enemiesProviders,
            IIndicatorHandler indicatorHandler,
            IHealthViewFactory healthViewFactory)
        {
            _healthViewFactory = healthViewFactory;
            _indicatorHandler = indicatorHandler;
            _enemiesProviders = enemiesProviders;
        }
        
        public void Initialize()
        {
            foreach (IEnemiesProvider enemiesProvider in _enemiesProviders)
            {
                enemiesProvider.Enemies.CollectionChanged += OnCollectionChanged;
            }
        }

        private void OnCollectionChanged(in NotifyCollectionChangedEventArgs<Enemy> e)
        {
            Enemy enemy;
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                enemy = e.NewItem;
                IView view = _healthViewFactory.Create();
                _indicatorHandler.Register(enemy, view);
            }

            if (e.Action != NotifyCollectionChangedAction.Remove)
                return;

            enemy = e.OldItem;
            _indicatorHandler.Unregister(enemy);
        }

        public void Dispose()
        {
            foreach (IEnemiesProvider enemiesProvider in _enemiesProviders)
            {
                enemiesProvider.Enemies.CollectionChanged -= OnCollectionChanged;
            }
        }
    }
}


