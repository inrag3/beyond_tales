using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using _Project.Runtime.Infrastructure.Factories;
using _Project.Runtime.Infrastructure.Factories.UI;
using ObservableCollections;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Meta.Health
{
    public class EnemiesHealthPresenter : IInitializable, IDisposable
    {
        private readonly List<IEnemiesProvider> _enemiesProviders;
        private readonly IIndicatorHandler _indicatorHandler;
        private readonly IHealthViewFactory _healthViewFactory;
        private readonly Canvas _canvas;

        public EnemiesHealthPresenter(List<IEnemiesProvider> enemiesProviders,
            IIndicatorHandler indicatorHandler,
            IHealthViewFactory healthViewFactory,
            Canvas canvas)
        {
            _canvas = canvas;
            _healthViewFactory = healthViewFactory;
            _indicatorHandler = indicatorHandler;
            _enemiesProviders = enemiesProviders;
        }
        
        public void Initialize()
        {
            foreach (IEnemiesProvider enemiesProvider in _enemiesProviders)
            {
                enemiesProvider.Enemies.CollectionChanged += OnCollectionChanged;
                foreach (Enemy enemy in enemiesProvider.Enemies)
                {
                    Register(enemy);
                }
            }
            
        }

        private void OnCollectionChanged(in NotifyCollectionChangedEventArgs<Enemy> e)
        {
            Enemy enemy;
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                enemy = e.NewItem;
                Register(enemy);
            }

            if (e.Action != NotifyCollectionChangedAction.Remove)
                return;

            enemy = e.OldItem;
            _indicatorHandler.Unregister(enemy);
        }

        private void Register(Enemy enemy)
        {
            IView view = _healthViewFactory.Create(_canvas.transform);
            _indicatorHandler.Register(enemy.Point, view);
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


