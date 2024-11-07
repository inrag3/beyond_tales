using System;
using _Project.Runtime.Config.Herbalist;
using _Project.Runtime.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Herbalist
{
    public sealed class Mover : ITickable
    {
        private readonly int _speed;
        private readonly IInputService _inputService;
        private readonly IHerbalistProvider _provider;
        private readonly Animer _animer;
        public Mover(
            ISpeedConfig config,
            Animer animer,
            IHerbalistProvider herbalistProvider,
            IInputService inputService
        )
        {
            _provider = herbalistProvider;
            _inputService = inputService;
            _animer = animer;
            _speed = config.Speed;
        }
        public void Tick()
        {
            float moveHorizontal = _inputService.Horizontal;
            float moveVertical = _inputService.Vertical;

            var movement = new Vector3(moveHorizontal, 0f, moveVertical);
            movement = Quaternion.Euler(0, 45, 0) * movement;

            float value = Math.Clamp(movement.magnitude, 0, 1);
            _animer.PlayMove(value);
            if (!(movement.magnitude > 0))
                return;

            Transform transform = _provider.Herbalist.Transform;
            transform.position += movement * (_speed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}