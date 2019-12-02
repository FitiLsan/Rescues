using UnityEngine;


namespace Rescues
{
    public sealed class Character
    {
        private float _speed;
        private Vector3 _direction;
        private Rigidbody2D _rigidbody2D;
        private Transform _transform;

        private bool _isForward = true;

        public Character(Transform transform, PlayerData playerData)
        {
            _direction = Vector3.zero;
            _speed = 5.0f;
            _transform = transform;
            _rigidbody2D = _transform.GetComponent<Rigidbody2D>();
        }

        public void Move(float direction)
        {        
            _direction.x = direction * _speed;
            _direction.y = _rigidbody2D.velocity.y;

            _rigidbody2D.velocity = _direction;

            if (_rigidbody2D.velocity.x > 0 && !_isForward) Flip();
            else if(_rigidbody2D.velocity.x < 0 && _isForward) Flip();
        }

        void Flip()
        {
            _isForward = !_isForward;
            Vector3 dir = _transform.localScale;
            dir.x *= -1;
            _transform.localScale = dir;
        }
    }
}
