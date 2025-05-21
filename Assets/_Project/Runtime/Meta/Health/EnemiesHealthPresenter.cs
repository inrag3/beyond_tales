using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using _Project.Runtime.Core.Enemies;
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
        private IHealthPresenterFactory _presenterFactory;

        public EnemiesHealthPresenter(List<IEnemiesProvider> enemiesProviders,
            IIndicatorHandler indicatorHandler,
            IHealthViewFactory healthViewFactory,
            IHealthPresenterFactory presenterFactory,
            Canvas canvas)
        {
            _presenterFactory = presenterFactory;
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
            HealthView view = _healthViewFactory.Create(_canvas.transform);
            view.Transform.SetAsFirstSibling();
            _presenterFactory.Create(view, enemy.Health);
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


