using System;
using UnityEngine;


namespace Rescues
{
    public sealed class CharacterModel
    {
        #region Fields

        private readonly float _speed;
        private SpriteRenderer _characterSprite;
        private CapsuleCollider2D _playerCollider;
        private Rigidbody2D _playerRigidbody2D;
        private State _state;
        public Timer AnimationPlay;
        private Vector2 _direction;
        private Vector3 _teleportPosition;
        private HidingPlaceBehaviour _hidingPlaceBehaviour;

        #endregion


        #region Properties

        public Transform Transform { get; }
        private PlayerBehaviour PlayerBehaviour { get; }
        public AudioSource PlayerSound { get; }
        public float AnimationTimer { get; set; }
        public State PlayerState { get { return _state; } }

        #endregion


        #region ClassLifeCycle

        public CharacterModel(Transform transform, PlayerData playerData)
        {
            _speed = playerData.Speed;
            _characterSprite = transform.GetComponent<SpriteRenderer>();
            _playerCollider = transform.GetComponent<CapsuleCollider2D>();
            _playerRigidbody2D = transform.GetComponent<Rigidbody2D>();
            AnimationPlay = new Timer();
            Transform = transform;
            PlayerSound = Transform.GetComponent<AudioSource>();
            PlayerBehaviour = Transform.GetComponent<PlayerBehaviour>();

        }

        #endregion


        #region StateMachine     

        public void StateIdle()
        {
            SetState(State.Idle);
        }

        public void StateHideAnimation(HidingPlaceBehaviour hidingPlaceBehaviour)
        {
            switch (_state)
            {
                case State.Hiding:
                    {
                        StartHiding();
                        return;
                    }                   
            }
            SetState(State.HideAnimation);
            if (hidingPlaceBehaviour != null)
            {
                _hidingPlaceBehaviour = hidingPlaceBehaviour;
            }
            StartHiding();
        }

        public void StateHiding()
        {
            switch (_state)
            {
                case State.HideAnimation:
                    {
                        Hide();
                    }
                    break;
                case State.Hiding:
                    {
                        StateIdle();
                        Hide();
                        return;
                    }                   
            }
            SetState(State.Hiding);
        }

        public void StateTeleporting(Vector3 position)
        {
            SetState(State.Teleporting);
            _teleportPosition = position;
        }

        public void StateMoving(Vector2 direction)
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
            }
            SetState(State.Moving);
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
                case State.Teleporting:
                    {
                        Teleport();
                    }
                    break;
                case State.Moving:
                    {
                        Move();
                    }
                    break;
            }
        }

        #endregion


        #region Methods 

        private void Teleport()
        {
            Transform.position = _teleportPosition;
        }

        private void StartHiding()
        {           
            PlayerSound.clip = _hidingPlaceBehaviour.HidingPlaceData.HidingSound;
            AnimationTimer = _hidingPlaceBehaviour.HidingPlaceData.AnimationDuration;
            PlayAnimationWithTimer();
        }

        private void Hide()
        {            
            _playerCollider.enabled = !_playerCollider.enabled;
            if (_playerRigidbody2D.bodyType == RigidbodyType2D.Dynamic)
            {
                _playerRigidbody2D.bodyType = RigidbodyType2D.Static;
            }
            else _playerRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }

        private void PlayAnimationWithTimer()
        {
            if (PlayerSound.clip != null)
            {
                PlayerSound.Play();
            }
            Debug.Log(AnimationTimer);
            AnimationPlay.StartTimer(AnimationTimer);
        }

        private void Move()
        {
            _direction *= _speed * Time.deltaTime;

            Transform.Translate(_direction);

            if (_direction.x > 0 && _characterSprite.flipX)
            {
                Flip();
            }
            else if (_direction.x < 0 && !_characterSprite.flipX)
            {
                Flip();
            }
        }

        private void Flip()
        {
            _characterSprite.flipX = !_characterSprite.flipX;
        }

        #endregion
    }
}
