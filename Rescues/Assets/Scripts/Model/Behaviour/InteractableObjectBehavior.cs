using System;
using UnityEngine;


namespace Rescues
{
    public class InteractableObjectBehavior : MonoBehaviour, ITrigger
    {
        #region Fields

        [SerializeField] private InteractableObjectType _type;
        [SerializeField] private InteractionPrompt _interactionPrompt;
        [SerializeField] private Vector2 _promptOffset;

        #endregion


        #region Properties

        public Predicate<Collider2D> OnFilterHandler { get; set; }
        public Action<ITrigger> OnTriggerEnterHandler { get; set; }
        public Action<ITrigger> OnTriggerExitHandler { get; set; }
        public Action<ITrigger, InteractableObjectType> DestroyHandler { get; set; }
        public GameObject GameObject => gameObject;
        public InteractableObjectType Type { get => _type; set => _type = value; }
        public bool IsInteractable { get; set; }
        [field: SerializeField] public bool IsInteractionLocked { get; set; }
        public string Description { get; set; }
        [field: SerializeField] public string Id { get; set; } = "-1";
        public InteractionPrompt InteractionPrompt => _interactionPrompt;
        public Vector2 PromptOffset => _promptOffset;

        #endregion


        #region UnityMethods

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (OnFilterHandler?.Invoke(other) == true)
            {
                OnTriggerEnterHandler?.Invoke(this);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (OnFilterHandler?.Invoke(other) == true)
            {
                OnTriggerExitHandler?.Invoke(this);
            }
        }

        private void OnDisable()
        {
            DestroyHandler?.Invoke(this, _type);
        }

        #endregion
    }
}
