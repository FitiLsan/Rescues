using UnityEngine;


namespace Rescues
{
    public sealed class Character
    {
        #region Fields

        private Vector3 _direction;
        private readonly float _speed;
        private bool _isForward = true;

        #endregion


        #region Properties

        private Rigidbody2D Rigidbody2D { get; }
        private Transform Transform { get; }
        private PlayerBehaviour PlayerBehaviour { get; }

        #endregion


        #region ClassLifeCycle

        public Character(Transform transform, PlayerData playerData)
        {
            _direction = Vector3.zero;
            _speed = playerData.Speed;
            Transform = transform;
            Rigidbody2D = Transform.GetComponent<Rigidbody2D>();
            PlayerBehaviour = Transform.GetComponent<PlayerBehaviour>();
        }

        #endregion


        #region Methods

        public void Teleport(Vector3 position)
        {
            Transform.position = position;
        }

        public void Move(float direction)
        {        
            _direction.x = direction * _speed;
            _direction.y = Rigidbody2D.velocity.y;

            Rigidbody2D.velocity = _direction;

            if (Rigidbody2D.velocity.x > 0 && !_isForward)
            {
                Flip();
            }
            else if(Rigidbody2D.velocity.x < 0 && _isForward)
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
