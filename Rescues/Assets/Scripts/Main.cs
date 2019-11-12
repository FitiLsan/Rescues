using UnityEngine;


namespace Rescues
{
    public class Main : MonoBehaviour
    {
        private InputController InputController { get; set; }

        #region UnityMethods
        private void Awake()
        {
            InputController = new InputController();
            InputController.Character = FindObjectOfType<Character>();
        }

        private void Update()
        {
            InputController.OnUpdate();
        }
        #endregion
    }
}