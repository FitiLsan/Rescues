using System;
using PathCreation;
using UnityEngine;


namespace Rescues
{
    public sealed class CharacterModel
    {
        #region Fields

        private readonly int _speed;
        private SpriteRenderer _characterSprite;
        private CapsuleCollider2D _playerCollider;
        private Rigidbody2D _playerRigidbody2D;
        private State _state;
        public Timer AnimationPlayTimer;
        private int _direction;
        private Gate _gate;
        private HidingPlaceBehaviour _hidingPlaceBehaviour;
        private Animator _animator;
        private CurveWay _curveWay;
        private int _currentCurveWayPoint;
        private float move = 0f;
        
        #endregion


        #region Properties
        public Transform Transform { get; }
        private PlayerBehaviour PlayerBehaviour { get; }
        public AudioSource PlayerSound { get; }
        public float AnimationTimer { get; set; }
        public State PlayerState { get { return _state; } }
        public InteractableObjectBehavior InteractableItem { get; set; }
        public CurveWay CurentCurveWay => _curveWay;

        #endregion


        #region ClassLifeCycle

        public CharacterModel(Transform transform, PlayerData playerData)
        {
            _speed = (int)playerData.Speed;
            _characterSprite = transform.GetComponentInChildren<SpriteRenderer>();
            _playerCollider = transform.GetComponentInChildren<CapsuleCollider2D>();
            _playerRigidbody2D = transform.GetComponentInChildren<Rigidbody2D>();
            _animator = transform.GetComponentInChildren<Animator>();
            AnimationPlayTimer = new Timer();
            Transform = transform;
            PlayerSound = Transform.GetComponentInChildren<AudioSource>();
            PlayerBehaviour = Transform.GetComponent<PlayerBehaviour>();
        }

        #endregion


        #region StateMachine     

        public void StateIdle()
        {
            SetState(State.Idle);
            _animator.Play("Base Layer.Idle");
        }

        public void StateHideAnimation(HidingPlaceBehaviour hidingPlaceBehaviour)
        {
            if (hidingPlaceBehaviour != null)
            {
                _hidingPlaceBehaviour = hidingPlaceBehaviour;
            }           
            SetState(State.HideAnimation);                                           
            StartHiding();
            _animator.Play("Base Layer.HideAnimation");
        }

        public void StateHiding()
        {                            
            if (Hide())
            {
                _animator.Play("Base Layer.Hiding");
                SetState(State.Hiding);                             
            }
            else
            {
                StateIdle();                
            }
        }

        public void StatePickUpAnimation(ItemBehaviour itemBehaviour)
        {
            InteractableItem = itemBehaviour;
            SetState(State.PickUpAnimation);
            _animator.Play("Base Layer.PickUpAnimation");
            AnimationPlayTimer.StartTimer(itemBehaviour.PickUpTime);           
        }

        public void StateCraftTrapAnimation(TrapBehaviour trapBehaviour)
        {
            InteractableItem = trapBehaviour;
            SetState(State.CraftTrapAnimation);
            _animator.Play("Base Layer.CraftTrapAnimation");
            AnimationPlayTimer.StartTimer(trapBehaviour.TrapInfo.BaseTrapData.CraftingTime);
        }

        public void StateTeleporting(Gate gate)
        {
            SetState(State.GoByGateWay);
            _gate = gate;
            _animator.Play("Base Layer.Teleporting");
            AnimationPlayTimer.StartTimer(gate.LocalTransferTime);
        }

        public void StateMoving(int direction)
        {
            switch (_state)
            {
                case State.Hiding:
                    {                       
                        return;
                    }
                case State.HideAnimation:
                    {                        
                        return;
                    }
                case State.PickUpAnimation:
                    {
                        return;
                    }
                case State.CraftTrapAnimation:
                    {
                        return;
                    }
                case State.GoByGateWay:
                    {
                        return;
                    }
            }
            SetState(State.Moving);
            _animator.Play("Base Layer.Moving");
            _direction = direction;
        }

        private void SetState(State value)
        {
            _state = value;
        }

        public void StateHandler()
        {
            switch (_state)
            {
                case State.Moving:
                    Move();
                    break;

                case State.GoByGateWay:
                    GoByGateWay();
                    break;
            }
        }

        #endregion


        #region Methods

        public void GoByGateWay()
        {
            _gate.GoByGateWay();
        }

        private void StartHiding()
        {
            PlayerSound.clip = _hidingPlaceBehaviour.HidingPlaceData.HidingSound;
            AnimationTimer = _hidingPlaceBehaviour.HidingPlaceData.AnimationDuration;         
            PlayAnimationWithTimer();
            if (_playerRigidbody2D.bodyType == RigidbodyType2D.Dynamic)
            {
                _characterSprite.enabled = false; //чтобы спрайт выключался сразу, когда идет процесс пряток
            }
            else
            {
                _hidingPlaceBehaviour.HidedSprite.enabled = false; //чтобы спрайт хайдинг плейс бехевора выключался сразу, когда персонаж начинает вылезать
            }
        }

        private bool Hide()
        {
            bool isHided;
            _playerCollider.enabled = !_playerCollider.enabled;          
            if (_playerRigidbody2D.bodyType == RigidbodyType2D.Dynamic)
            {
                _playerRigidbody2D.bodyType = RigidbodyType2D.Static;
                isHided = true;
                _hidingPlaceBehaviour.HidedSprite.enabled = true; //чтобы спрайт хайдинг плейс бехевора включался только тогда, когда персонаж спрятался
            }
            else
            {
                _playerRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                isHided = false;
                _characterSprite.enabled = true; //чтобы спрайт выключался только тогда, когда персонаж уже вылез
            }

            return isHided;
        }

        private void PlayAnimationWithTimer()
        {
            if (PlayerSound.clip != null)
            {
                PlayerSound.Play();
            }          
            AnimationPlayTimer.StartTimer(AnimationTimer);
        }

        public void SetPositionAndCurveWay(CurveWay curveWay)
        {
            _curveWay = curveWay;
            Transform.position = curveWay.GetStartPointPosition;
        }
        
        private void Move()
        {
            move += _direction * _speed * Time.deltaTime;
            Transform.position = _curveWay.PathCreator.path.GetPointAtDistance(move, EndOfPathInstruction.Stop);

            if (_direction == 0)
            {
                StateIdle();               
            }

            if (_direction > 0 && _characterSprite.flipX)
            {
                Flip();               
            }
            else if (_direction < 0 && !_characterSprite.flipX)
            {
                Flip();            
            }
        }

        public void SetScale()
        {
            Transform.localScale =  _curveWay.GetScale(Transform.position);
        }
        
        private void Flip()
        {
            _characterSprite.flipX = !_characterSprite.flipX;
        }

        #endregion

       
    }
}
