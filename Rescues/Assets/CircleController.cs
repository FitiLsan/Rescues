//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;


namespace Rescues
{

    public class CircleController : IExecute
    {
        private readonly GameContext _context;
        public CircleController(GameContext context, Services services)
        {
            _context = context;
        }

        public float Speed = 30.0f;

        private Vector3 _dir = Vector3.zero;



        // Start is called before the first frame update


        // Update is called once per frame
        public void metod()
        {
            _dir.x = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
            _dir.y = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
            //transform.position += _dir;
            if (_dir.x != 0 || _dir.y != 0)
            {
                _context.Character.Move(_dir);
            }


        }
    }
}
