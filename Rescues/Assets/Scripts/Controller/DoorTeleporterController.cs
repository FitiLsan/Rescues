using DG.Tweening;
using UnityEngine;

namespace Rescues
{
    public sealed class DoorTeleporterController : IInitializeController, ITearDownController
    {
        private readonly GameContext _context;

        public DoorTeleporterController(GameContext context)
        {
            _context = context;
        }
        
        public void Initialize()
        {
            foreach (var trigger in _context.OnTriggers)
            {
                if (trigger is DoorTeleporterBehaviour doorTeleporter)
                {
                    doorTeleporter.OnTriggerEnterHandler += OnTriggerEnterHandler;
                    doorTeleporter.OnTriggerExitHandler += OnTriggerExitHandler;
                }
            }
        }

        public void TearDown()
        {     
            foreach (var trigger in _context.OnTriggers)
            {
                if (trigger is DoorTeleporterBehaviour doorTeleporter)
                {
                    doorTeleporter.OnTriggerEnterHandler -= OnTriggerEnterHandler;
                    doorTeleporter.OnTriggerExitHandler -= OnTriggerExitHandler;
                }
            }
        }

        private void OnTriggerEnterHandler(IOnTrigger obj)
        {
            var materialColor = obj.GameObject.GetComponent<SpriteRenderer>().color;
            obj.GameObject.GetComponent<SpriteRenderer>().DOColor(new Color(materialColor.r,
            materialColor.g, materialColor.b, 0.5f), 1.0f);
        }

        private void OnTriggerExitHandler(IOnTrigger obj)
        {
            var materialColor = obj.GameObject.GetComponent<SpriteRenderer>().color;
            obj.GameObject.GetComponent<SpriteRenderer>().DOColor(new Color(materialColor.r,
                materialColor.g, materialColor.b, 1.0f), 1.0f);
        }
    }
}
