using UnityEngine;


namespace Rescues
{
    public sealed class InputController : IExecuteController
    {
        #region Fields

        private Vector3 _direction;
        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles
        
        public InputController(GameContext context, Services services)
        {
            _context = context;
        }

        #endregion


        #region IExecuteController
        
        public void Execute()
        {
            _context.Character.Move(Input.GetAxis("Horizontal"));

            if (_direction != 0)
            {
                _context.Character.Move();
            }
        }

        #endregion
    }
}
