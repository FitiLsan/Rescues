using UnityEngine;


namespace Rescues
{
    public sealed class Character
    {
        #region PrivateData
        private Vector3 _direction;
        private readonly float _speed;
        private float _horizontal;
        private Rigidbody2D Rigidbody2D { get; }
        private Transform Transform { get; }
        private PlayerBehaviour PlayerBehaviour { get; }

        private bool _isForward = true;
        #endregion
        #region UnityMethods
        void Update()
        {
            _horizontal = Input.GetAxis("Horizontal");
            if(_horizontal != 0)
            {
                Move(1);
            }
        }
        #endregion
        #region Methods
        public Character(Transform transform, PlayerData playerData)
        {
            _direction = Vector3.zero;
            _speed = playerData.Speed;
            Transform = transform;
            Rigidbody2D = Transform.GetComponent<Rigidbody2D>();
            PlayerBehaviour = Transform.GetComponent<PlayerBehaviour>();
        }

        public void Move(float direction)
        {
            RaycastHit2D hit = Physics2D.Raycast(Transform.position, Vector2.down, 9);
            if (hit != true) Transform.Translate(new Vector3(0.0f, -0.1f, 0.0f));
            Transform.Translate(new Vector3(_horizontal, 0.0f, 0.0f));
            _direction.x = direction * _speed;
            _direction.y = Rigidbody2D.velocity.y;

            Rigidbody2D.velocity = _direction;

            if (Rigidbody2D.velocity.x > 0 && !_isForward)
            {
                Flip();
            }
            else if (Rigidbody2D.velocity.x < 0 && _isForward)
            {
                Flip();
            
  
        }
    }

        private void Flip()
        {
            _isForward = !_isForward;
            Vector3 dir = Transform.localScale;
            dir.x *= -1;
            Transform.localScale = dir;
        }
        #endregion
    }
}
