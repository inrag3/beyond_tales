using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using _Project.Runtime.Infrastructure.Factories;
using _Project.Runtime.Infrastructure.Factories.UI;
using ObservableCollections;
using Zenject;

namespace _Project.Runtime.Infrastructure.Installers.SceneInstallers
{
    public class EnemiesHealthPresenter : IInitializable, IDisposable
    {
        private readonly List<IEnemiesProvider> _enemiesProviders;
        private readonly IndicatorHandler _indicatorHandler;
        private readonly IHealthViewFactory _healthViewFactory;

        public EnemiesHealthPresenter(List<IEnemiesProvider> enemiesProviders,
            IndicatorHandler indicatorHandler,
            IHealthViewFactory healthViewFactory)
        {
            _healthViewFactory = healthViewFactory;
            _indicatorHandler = indicatorHandler;
            _enemiesProviders = enemiesProviders;
        }
        
        public void Initialize()
        {
            foreach (var enemiesProvider in _enemiesProviders)
            {
                enemiesProvider.Enemies.CollectionChanged += OnCollectionChanged;
            }
        }

        private void OnCollectionChanged(in NotifyCollectionChangedEventArgs<Enemy> e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Enemy enemy in e.NewItems)
                {
                    var view = _healthViewFactory.Create();
                    _indicatorHandler.Register(enemy, view);
                }
            }

            if (e.Action != NotifyCollectionChangedAction.Remove)
                return;
            
            foreach (Enemy enemy in e.OldItems)
            {
                _indicatorHandler.Unregister(enemy);
            }
        }

        public void Dispose()
        {
            foreach (var enemiesProvider in _enemiesProviders)
            {
                enemiesProvider.Enemies.CollectionChanged -= OnCollectionChanged;
            }
        }
    }
}