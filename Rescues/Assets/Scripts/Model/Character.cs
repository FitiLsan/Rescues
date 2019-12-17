using UnityEngine;


namespace Rescues
{
    public sealed class Character
    {
        #region Fields

        private readonly float _speed;
        private SpriteRenderer _characterSprite;

        #endregion


        #region Properties

        public Transform Transform { get; }
        private PlayerBehaviour PlayerBehaviour { get; }

        #endregion


        #region ClassLifeCycle

        public Character(Transform transform, PlayerData playerData)
        {
            _speed = playerData.Speed;
            _characterSprite = transform.GetComponent<SpriteRenderer>();           
            Transform = transform;
            PlayerBehaviour = Transform.GetComponent<PlayerBehaviour>();
        }

        #endregion


        #region Methods

        public void Teleport(Vector3 position)
        {
            Transform.position = position;
        }

        public void Move(Vector2 direction)
        {        
            direction *= _speed * Time.deltaTime;

            Transform.Translate(direction);

            if (direction.x > 0 && _characterSprite.flipX)
            {
                Flip();
            }
            else if(direction.x < 0 && !_characterSprite.flipX)
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
