using System;
using _Project.Runtime.AI.Implementation;
using _Project.Runtime.Core.Herbalist;
using Extensions;
using UnityEngine;

namespace _Project.Runtime.Core.Enemies
{
    public class Hut : Enemy
    {
        [SerializeField ]private Vector2 _followDistanceRange = new Vector2(3,5);

        public Vector2 FollowDistanceRange => _followDistanceRange;
        
        [SerializeField] private float _backwardMovementSpeed = 2f;

        public float BackwardMovementSpeed => _backwardMovementSpeed;

        [SerializeField] private float _timeInRangeToStartAttack = 1f;

        public float TimeInRangeToStartAttack => _timeInRangeToStartAttack;
        
        [SerializeField] private float _runAwayCheckDistance = 1.5f;

        public float RunAwayCheckDistance => _runAwayCheckDistance;
        
       [SerializeField]  private float _timeOnPrepareJump = 0.4f;
       [SerializeField] private float _timeOnEndJump = 0.3f;
       [SerializeField] private float _jumpGravity = 10;
       [SerializeField] private float _timeInJump = 1f;
       [SerializeField] private float _attackRange = 2f;
       [SerializeField] private float _attackDamage = 50;
       
       private float _timeInAttackState;
       private float _currentVerticalJumpSpeed;
       private Vector2 _horizontalJumpSpeed;

        private HutBehaviourRule _behaviourRule;

        public IHerbalist PlayerTarget => _provider.Herbalist;

        public Animer Animer => _animer;

        public Movement Movement => _movement;

        private Vector3 _savePos;

        private bool _inJumpAttack = false;

        private Rigidbody _body;

        private bool _isPaused = false;

        private bool _attackDamageApplied = false;

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
        }

        public void AssignBehaviour(HutBehaviourRule behaviourRule)
        {
            _behaviourRule = behaviourRule;
        }

        public void MoveTo(Vector3 position)
        {
            _savePos = position;
            _movement.Move(position);
        }

        public void Stop()
        {
            _animer.PlayIdle();
            _movement.Pause();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_savePos, 0.5f);

            if (_inJumpAttack)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(transform.position,new Vector3(_horizontalJumpSpeed.x, _currentVerticalJumpSpeed,
                    _horizontalJumpSpeed.y) );
            }
        }

        public void StartJumpAttack()
        {
            //Debug.Log($"Start jump attack");
            _inJumpAttack = true;
            _attackDamageApplied = false;
            _timeInAttackState = 0;
            _currentVerticalJumpSpeed = _jumpGravity * _timeInJump / 2;
            _horizontalJumpSpeed =
                (PlayerTarget.Transform.position - Transform.position).ToXZ() / _timeInJump;
            Animer.SetTrigger("StartJump");
            Movement.StopUpdateRotation();
            Movement.StopUpdatePosition();
            _body.isKinematic = true;
            _body.useGravity = false;
        }

        private void EndAttackJump()
        {
            if (_inJumpAttack)
            {
                _inJumpAttack = false;
                _behaviourRule.EndJumpAttack();
                _body.useGravity = true;
                _body.isKinematic = false;
                Movement.ResumeUpdateRotation();
                Movement.ResumeUpdatePosition();
            }
        }

        private void Update()
        {
            if (_inJumpAttack && !_isPaused)
            {
                //Debug.Log($"Update in jump attack");
                _timeInAttackState += Time.deltaTime;
                
                var targetRot = Quaternion.LookRotation(
                    new Vector3(_horizontalJumpSpeed.x,0,_horizontalJumpSpeed.y),
                    Vector3.up);
                Transform.rotation = Quaternion.RotateTowards(Transform.rotation, targetRot,
                    Movement.AngularSpeed * Time.deltaTime);

                if (_timeInAttackState < _timeOnPrepareJump)
                {

                }
                else if (_timeInAttackState > _timeOnPrepareJump &&
                         _timeInAttackState < _timeOnPrepareJump + _timeInJump)
                {
                    Animer.SetBool("InMiddleJump", true);
                    _currentVerticalJumpSpeed -= _jumpGravity * Time.deltaTime;
                    Transform.position += (new Vector3(_horizontalJumpSpeed.x, _currentVerticalJumpSpeed,
                        _horizontalJumpSpeed.y) * Time.deltaTime);
                }
                else if (_timeInAttackState < _timeOnPrepareJump + _timeInJump + _timeOnEndJump)
                {
                    if (!_attackDamageApplied)
                    {
                        if (Vector3.SqrMagnitude(PlayerTarget.Transform.position - transform.position) <=
                            Mathf.Pow(_attackRange, 2f))
                        {
                            PlayerTarget.TakeDamage(_attackDamage);
                            _attackDamageApplied = true;
                        }
                    }

                    Animer.SetBool("InMiddleJump", false);
                    Animer.SetBool("EndJump", true);
                }
                else
                {
                    //Debug.Log($"End attack jump");
                    EndAttackJump();
                }
            }
        }

        public override void Pause()
        {
            base.Pause();
            _isPaused = true;
        }
        
        public override void Resume()
        {
            base.Resume();
            _isPaused = false;
        }
    }
}