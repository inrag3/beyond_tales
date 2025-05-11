using System;
using _Project.Runtime.Config.Herbalist;
using _Project.Runtime.Core.PauseHandler;
using _Project.Runtime.Infrastructure.Factories;
using Extensions;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Herbalist
{
    public sealed class Mover : ITickable, IPauseHandler
    {
        private readonly int _speed;
        private readonly IInputService _inputService;
        private readonly IHerbalistProvider _provider;
        private readonly HerbalistAnimer _animer;
        private readonly PauseHandlersRegister _pauseHandlersRegister;
        private CharacterController _controller;
        
        private const float GRAVITY = -9.81f;
        private Vector3 _velocity;
        private bool _isPaused;

        private bool _isMoving;

        public bool IsMoving => !_isPaused && _isMoving;

        public Mover(
            ISpeedConfig config,
            HerbalistAnimer animer,
            IHerbalistProvider herbalistProvider,
            CharacterController controller,
            IInputService inputService,
            PauseHandlersRegister pauseHandlersRegister
        )
        {
            _controller = controller;
            _provider = herbalistProvider;
            _inputService = inputService;
            _animer = animer;
            _speed = config.Speed;
            _pauseHandlersRegister = pauseHandlersRegister;
            _pauseHandlersRegister.RegisterPauseHandler(this);
        }
        
        
        public void Tick()
        {
            if (_isPaused)
                return;
            
            Transform transform = _provider.Herbalist.Transform;
            
            float moveHorizontal = _inputService.Horizontal;
            float moveVertical = _inputService.Vertical;

            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
            movement = Quaternion.Euler(0, 45, 0) * movement;
            movement.Normalize();

            float value = Mathf.Clamp(movement.magnitude, 0, 1);
            _animer.PlayMove(value);

            // Применение гравитации
            if (_controller.isGrounded && _velocity.y < 0)
                _velocity.y = -2f; // Небольшой толчок вниз для надежного приземления

            _velocity.y += GRAVITY * Time.deltaTime;

            if (_inputService.IsRollButtonPressed && movement.magnitude > 0)
            {
                _animer.PlayRoll(OnRollCompleted);
                
                _controller.Move(movement * (8 * Time.deltaTime)); // Ускоренное перекатывание
                _controller.height = 0.9f;
                _controller.center = new Vector3(_controller.center.x, 0.55f, _controller.center.z);
            }
            else
            {
                _controller.Move(movement * (_speed * Time.deltaTime)); // Обычное движение
            }

            _controller.Move(_velocity * Time.deltaTime); // Применяем гравитацию

            // Поворот персонажа в направлении движения
            if (!(movement.magnitude > 0))
            {
                _isMoving = false;
                return;
            }

            _isMoving = true;
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        private void OnRollCompleted()
        {
            _controller.height = 1.8f;
            _controller.center = new Vector3(_controller.center.x, 1f, _controller.center.z);
        }

        public void Pause()
        {
            //Debug.Log($"Pause mover!");
            _isPaused = true;
        }

        public void Resume()
        {
            _isPaused = false;
        }
    }
}