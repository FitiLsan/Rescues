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
        public Timer AnimationPlay;        

        #endregion


        #region Properties

        public Transform Transform { get; }
        private PlayerBehaviour PlayerBehaviour { get; }
        public AudioSource PlayerSound { get; }        
        public bool IsColliderOn { get; set; }
        public bool IsHiding { get; set; }
        public bool IsPlayingAnimation { get; set; }
        public float AnimationTimer { get; set; }

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
            IsColliderOn = true;
            IsHiding = false;
            IsPlayingAnimation = false;
        }

        #endregion


        #region Methods

        public void Teleport(Vector3 position)
        {
            Transform.position = position;
        }

        public void StartHiding(HidingPlaceBehaviour hidingPlaceBehaviour)
        {
            PlayerSound.clip = hidingPlaceBehaviour.HidingPlaceData.HidingSound;
            AnimationTimer = hidingPlaceBehaviour.HidingPlaceData.AnimationDuration;
            PlayAnimationWithTimer();
            IsHiding = true;            
        }

        public void Hiding()
        {
            CustomDebug.Log("Спрятался/Вылез");
            IsColliderOn = !IsColliderOn;
            _playerCollider.enabled = !_playerCollider.enabled;
            if (_playerRigidbody2D.bodyType == RigidbodyType2D.Dynamic)
            {
                _playerRigidbody2D.bodyType = RigidbodyType2D.Static;
            }
            else _playerRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }

        public void PlayAnimationWithTimer()
        {
            if (PlayerSound.clip != null)
            {
                PlayerSound.Play();
            }
            AnimationPlay.StartTimer(AnimationTimer);
            IsPlayingAnimation = true;
        }

        public void Move(Vector2 direction)
        {
            if (IsColliderOn == true && IsPlayingAnimation == false)
            {
                direction *= _speed * Time.deltaTime;

                Transform.Translate(direction);

                if (direction.x > 0 && _characterSprite.flipX)
                {
                    Flip();
                }
                else if (direction.x < 0 && !_characterSprite.flipX)
                {
                    Flip();
                }
            }
        }

        private void Flip()
        {
            _characterSprite.flipX = !_characterSprite.flipX;
        }

        #endregion
    }
}
