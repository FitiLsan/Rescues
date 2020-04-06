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

        #endregion


        #region Properties

        public Transform Transform { get; }
        private PlayerBehaviour PlayerBehaviour { get; }
        public bool IsHided { get; set; }

        #endregion


        #region ClassLifeCycle

        public CharacterModel(Transform transform, PlayerData playerData)
        {
            _speed = playerData.Speed;
            _characterSprite = transform.GetComponent<SpriteRenderer>();
            _playerCollider = transform.GetComponent<CapsuleCollider2D>();
            _playerRigidbody2D = transform.GetComponent<Rigidbody2D>();
            Transform = transform;
            PlayerBehaviour = Transform.GetComponent<PlayerBehaviour>();
            IsHided = false;
        }

        #endregion


        #region Methods

        public void Teleport(Vector3 position)
        {
            Transform.position = position;
        }

        public void Hide()
        {
            IsHided = true;
            _playerCollider.enabled = false;
            _playerRigidbody2D.bodyType = RigidbodyType2D.Static;           
        }

        public void UnHide()
        {
            IsHided = false;
            _playerCollider.enabled = true;
            _playerRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }

        public void Move(Vector2 direction)
        {
            if (IsHided == false)
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
