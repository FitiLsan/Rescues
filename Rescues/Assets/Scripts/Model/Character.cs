using UnityEngine;


namespace Rescues
{
    public sealed class Character
    {
        private Vector3 _direction;
        private readonly float _speed;
        private Rigidbody2D Rigidbody2D { get; }
        public Transform Transform { get; }
        private PlayerBehaviour PlayerBehaviour { get; }

        private bool _isForward = true;

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
    }
}
