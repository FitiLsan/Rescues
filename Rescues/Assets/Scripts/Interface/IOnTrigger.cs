using System;
using UnityEngine;

namespace Rescues
{
    public interface IOnTrigger
    {
        Predicate<Collider2D> OnFilterHandler { get; set; }
        Action<IOnTrigger> OnTriggerEnterHandler { get; set; }
        Action<IOnTrigger> OnTriggerExitHandler { get; set; }
        bool IsInteractable { get; set; }
        GameObject GameObject { get; }
    }
}
