using UnityEngine;


namespace Rescues
{
    public sealed class Character
    {
        #region Fields

        private Vector3 _direction;
        private readonly float _speed;
        private float _raycastLength;
        private float _gravity;
        private SpriteRenderer _mySprite;

        #endregion


        #region Properties

        private Rigidbody2D Rigidbody2D { get; }
        private Transform Transform { get; }
        private PlayerBehaviour PlayerBehaviour { get; }

        #endregion


        #region ClassLifeCycles

        public Character(Transform transform, PlayerData playerData)
        {
            _direction = Vector3.zero;
            _speed = playerData.Speed;
            Transform = transform;
            Rigidbody2D = Transform.GetComponent<Rigidbody2D>();
            PlayerBehaviour = Transform.GetComponent<PlayerBehaviour>();
        }

        #endregion


        #region UnityMethods

        void Update()
        {
            _direction.x = Input.GetAxis("Horizontal");
            if (_direction != 0)
            {
                Move();
            }
        }

        #endregion


        #region Methods

        public void Move()
        {
            RaycastHit2D hit = Physics2D.Raycast(Transform.position, Vector2.down, _raycastLength);
            if (hit != true) Transform.Translate(Vector3.down * _gravity);
            Transform.Translate(new Vector3(_horizontal, 0.0f, 0.0f));
            if(_direction.x != 0)
            {
                if (_direction.x > 0 && !_isForward)
                {
                    Flip();
                }
                else if (_direction.x < 0 && _isForward)
                {
                    Flip();
                }
            }
    }

        private void Flip()
        {
            _isForward = !_isForward;
            _mySprite.flipX = !_mySprite.flipX;
        }

        #endregion
    }
}
