using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Rescues
{
    public sealed class DoorTeleporterController : IInitializeController, ITearDownController
    {
        private readonly GameContext _context;

        public DoorTeleporterController(GameContext context, Services services)
        {
            _context = context;
        }
        
        public void Initialize()
        {
            var doors = _context.GetTriggers<DoorTeleporterBehaviour>(TriggerObjectType.Door);
            foreach (var trigger in doors)
            {
                trigger.OnTriggerEnterHandler += OnTriggerEnterHandler;
                trigger.OnTriggerExitHandler += OnTriggerExitHandler;
            }
        }

        public void TearDown()
        {
            var doors = _context.GetTriggers<DoorTeleporterBehaviour>(TriggerObjectType.Door);
            foreach (var trigger in doors)
            {
                trigger.OnTriggerEnterHandler -= OnTriggerEnterHandler;
                trigger.OnTriggerExitHandler -= OnTriggerExitHandler;
            }
        }

        private void OnTriggerEnterHandler(IOnTrigger obj)
        {
            obj.IsInteractable = true;
            var materialColor = obj.GameObject.GetComponent<SpriteRenderer>().color;
            obj.GameObject.GetComponent<SpriteRenderer>().DOColor(new Color(materialColor.r,
            materialColor.g, materialColor.b, 0.5f), 1.0f);
        }

        private void OnTriggerExitHandler(IOnTrigger obj)
        {
            obj.IsInteractable = false;
            var materialColor = obj.GameObject.GetComponent<SpriteRenderer>().color;
            obj.GameObject.GetComponent<SpriteRenderer>().DOColor(new Color(materialColor.r,
                materialColor.g, materialColor.b, 1.0f), 1.0f);
        }
    }
}
