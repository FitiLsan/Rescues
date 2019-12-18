using System;
using UnityEngine;


namespace Rescues
{
    public interface ITrigger : IInteractable
    {
        Predicate<Collider2D> OnFilterHandler { get; set; }
        Action<ITrigger> OnTriggerEnterHandler { get; set; }
        Action<ITrigger> OnTriggerExitHandler { get; set; }
        Action<ITrigger, InteractableObjectType> DestroyHandler { get; set; }
        GameObject GameObject { get; }
        InteractableObjectType Type { get; }
    }
}
