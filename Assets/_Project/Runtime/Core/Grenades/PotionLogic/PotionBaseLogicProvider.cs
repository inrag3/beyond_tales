using System;
using _Project.Runtime.Config;
using _Project.Runtime.Infrastructure;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Grenades.PotionLogic
{
    public abstract class PotionBaseLogicProvider
    {
        protected abstract string GrenadePath { get; }
        protected abstract string ExplosionPath { get; }

        protected readonly IGrenadeConfig _grenadeConfig;
        protected readonly IAssetManager _assetManager;
        protected readonly IInstantiator _instantiator;
        protected readonly Vector3 _mouse;
        protected readonly Vector3 _throwerPosition;

        public PotionBaseLogicProvider(
            IGrenadeConfig grenadeConfig,
            IAssetManager assetManager,
            IInstantiator instantiator,
            Vector3 mouse,
            Vector3 throwerPosition
        )
        {
            _grenadeConfig = grenadeConfig;
            _assetManager = assetManager;
            _instantiator = instantiator;
            _mouse = mouse;
            _throwerPosition = throwerPosition;
        }

        public virtual void MakeAction()
        {
            ThrowGrenade();
        }

        protected void ThrowGrenade()
        {
            GameObject prefab = _assetManager.Get(GrenadePath);
            var grenade = _instantiator.InstantiatePrefabForComponent<Grenade>(prefab);
            grenade.transform.position = _throwerPosition;
            grenade.transform.position += Vector3.up;
            grenade.transform.parent = null;

            grenade.GetComponent<Rigidbody>().AddForce(GetGrenadeForceToAdd(grenade), ForceMode.Impulse);

            grenade.Hit += GrenadeContactCallback;
        }

        private void GrenadeContactCallback(Grenade grenade)
        {
            var explosion = CreateExplosion(grenade);
            explosion.Timer.TimeEnded += () =>
            {
                OnExplosionFinished(grenade, explosion);
                explosion.SelfDestroy();
            };

            explosion.Timer.Start(_grenadeConfig.GrenadeExplosionTimeout);
            OnExplosionStart(grenade, explosion);
        }

        protected virtual void OnExplosionFinished(Grenade grenade, GrenadeExplosion explosion)
        {
        }

        protected virtual void OnExplosionStart(Grenade grenade, GrenadeExplosion explosion)
        {
        }


        private GrenadeExplosion CreateExplosion(Grenade grenade)
        {
            GameObject prefab = _assetManager.Get(ExplosionPath);
            var explosion = _instantiator.InstantiatePrefabForComponent<GrenadeExplosion>(prefab);
            explosion.transform.parent = grenade.transform.parent;
            explosion.transform.position = grenade.transform.position;
            explosion.transform.rotation = grenade.transform.rotation;
            return explosion;
        }

        private Vector3 GetGrenadeForceToAdd(Grenade grenade)
        {
            Vector3 direction = _mouse - _throwerPosition;
            var distance = direction.magnitude;
            direction.Normalize();
            Vector3 forceToAdd;
            distance = Math.Min(distance, _grenadeConfig.GrenadeMaxDistance);

            //минутка фазики за 9й класс
            // В нашем случае Прикладывание силы - импульс
            //Считается по формуле U = m*v .m - масса, а v - скорость движения, но мы ее не знаем => v(гор) = U(гор)/m
            //движение по горизонтали считается равномерным, с скоростью v(гор), считается по формуле S=v(гор)*t  => t = S/v(гор)
            //в нашем случае S = distance. Мы нашли время, которое граната должна провести в полете, чтобы упасть ровно на курсор,
            //нужно узнать с какой силой (импульс) запустить гранату вверх, чтобы падение произошло через t секунд

            //Равноускоренное движение считается по формуле S = s0 + v*t + (a*t^2)/2
            //S - конечный путь, у нас - 0, потому что окажется на полу. 
            //s0 - начальное положение, высота по y
            //a = g, t уже знаем, нужно найти v
            // v(верт) = -(g * t / 2) - y0 / t;
            // и досчитываем импульс по первой формуле

            var y0 = grenade.transform.position.y;
            var g = Physics.gravity.y;
            var u1 = _grenadeConfig.GrenadeFrontForce;
            var m = grenade.GetComponent<Rigidbody>().mass;
            var t = (distance * m) / u1;
            var v2 = -(g * t / 2) - y0 / t;
            var u2 = m * v2;

            forceToAdd = direction * _grenadeConfig.GrenadeFrontForce +
                         grenade.transform.up * u2;
            return forceToAdd;
        }
    }
}