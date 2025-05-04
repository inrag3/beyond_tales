using _Project.Runtime.AI.Core;
using _Project.Runtime.Core.Enemies;
using Extensions;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.AI.Implementation
{
    public class HutBehaviourRule: IRule
    {
        public bool IsExecutable => true;

        private Hut _hut;

        private HutState _currentState = HutState.None;

        private Vector2 _followDistanceRange = new Vector2(4,6);
        private float _backwardMovementSpeed = 2f;
        private float _timeInRangeToStartAttack = 1f;
        private float _runAwayCheckDistance = 1.5f;

        private float _currentTimeInRange = 0f;

        private bool _isPaused;

        public HutBehaviourRule(Hut hut)
        {
            _hut = hut;
            _followDistanceRange = _hut.FollowDistanceRange;
            _timeInRangeToStartAttack = _hut.TimeInRangeToStartAttack;
            _runAwayCheckDistance = _hut.RunAwayCheckDistance;
            _backwardMovementSpeed = _hut.BackwardMovementSpeed;
            hut.AssignBehaviour(this);
        }

        public void Execute()
        {
            if (_currentState == HutState.None)
            {
                _currentState = HutState.FollowOnDistance;
                EnterFollowDistance();
            }

            if (!_isPaused)
            {
                UpdateState(_currentState);
            }
        }

        private void TransitionToState(HutState state)
        {
            //Debug.Log($"Transition from state {_currentState} to {state}");
            ExitState(_currentState);
            EnterState(state);
            _currentState = state;
        }

        private void EnterState(HutState state)
        {
            switch (state)
            {
                case HutState.Idle: EnterIdle(); break;
                case HutState.FollowOnDistance: EnterFollowDistance(); break;
                case HutState.Attack: EnterAttack(); break;
                case HutState.Stun: EnterStun(); break;
            }
        }

        private void UpdateState(HutState state)
        {
            switch (state)
            {
                case HutState.Idle: UpdateIdle(); break;
                case HutState.FollowOnDistance: UpdateFollowDistance(); break;
                case HutState.Attack: UpdateAttack(); break;
                case HutState.Stun: UpdateStun(); break;
            }
        }

        private void ExitState(HutState state)
        {
            switch (state)
            {
                case HutState.Idle: ExitIdle(); break;
                case HutState.FollowOnDistance: ExitFollowDistance(); break;
                case HutState.Attack: ExitAttack(); break;
                case HutState.Stun: ExitStun(); break;
            }
        }

        private void EnterIdle()
        {
            _hut.Stop();
        }
        
        private void EnterFollowDistance()
        {
            _currentTimeInRange = 0;
        }
        
        private void EnterAttack()
        {
            _hut.StartJumpAttack();
            _hut.Movement.IgnoreMovementAnimation(true);
        }
        
        private void EnterStun()
        {
            
        }
        
        private void ExitIdle()
        {
            
        }
        
        private void ExitFollowDistance()
        {
            _hut.Movement.ResumeUpdateRotation();
        }
        
        private void ExitAttack()
        {
            _hut.Animer.SetBool("EndJump", false);
            _hut.Movement.ResumeUpdateRotation();
            _hut.Movement.ResumeUpdatePosition();
            _hut.Movement.IgnoreMovementAnimation(false);
        }
        
        private void ExitStun()
        {
            
        }
        
        private void UpdateIdle()
        {
            
        }
        
        private void UpdateFollowDistance()
        {
            var dir = (_hut.PlayerTarget.Transform.position - _hut.Transform.position).ToXZ();
            var dist = dir.magnitude;
            //Debug.Log($"Follow dist = {dist}");
            if (dist < _followDistanceRange.x)
            {
                //Debug.Log($"follow less");
                _currentTimeInRange -= Time.deltaTime;
                if (_currentTimeInRange < 0)
                {
                    _currentTimeInRange = 0;
                }
                _hut.Movement.StopUpdateRotation();
                var targetRot = Quaternion.LookRotation(
                    _hut.PlayerTarget.Transform.position.SetY(_hut.Transform.position.y) - _hut.Transform.position,
                    Vector3.up);
                _hut.Transform.rotation = Quaternion.RotateTowards(_hut.Transform.rotation, targetRot,
                    _hut.Movement.AngularSpeed * Time.deltaTime);
                //_hut.Transform.LookAt(_hut.PlayerTarget.Transform.position.SetY(_hut.Transform.position.y),Vector3.up);
                var targetPos = (_hut.Transform.position - dir.normalized.ToVector3XZ() * _runAwayCheckDistance);
                if (_hut.Movement.CanAchievePos(targetPos))
                {
                    _hut.Movement.Move(targetPos, _backwardMovementSpeed);
                }
                else
                {
                    TransitionToState(HutState.Attack);
                }
            }
            else if(dist > _followDistanceRange.y)
            {
                //Debug.Log($"follow out");
                _currentTimeInRange -= Time.deltaTime;
                if (_currentTimeInRange < 0)
                {
                    _currentTimeInRange = 0;
                }
                _hut.Movement.ResumeUpdateRotation();
                _hut.MoveTo(_hut.PlayerTarget.Transform.position);
            }
            else
            {
                //Debug.Log($"follow in range");
                _currentTimeInRange += Time.deltaTime;
                if (_currentTimeInRange > _timeInRangeToStartAttack)
                {
                    TransitionToState(HutState.Attack);
                }
            }
        }
        
        private void UpdateAttack()
        {
            
        }
        
        private void UpdateStun()
        {
            
        }

        public void EndJumpAttack()
        {
            TransitionToState(HutState.FollowOnDistance);
        }

        enum HutState
        {
            None, Idle, FollowOnDistance, Attack, Stun
        }
    }
    
}