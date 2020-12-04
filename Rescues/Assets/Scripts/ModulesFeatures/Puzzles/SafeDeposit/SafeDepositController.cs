using System.Collections.Generic;
using UnityEngine;

namespace Rescues
{
    public class SafeDepositController:MonoBehaviour
    {
        #region Variebles
        private List<SafeBitePicBehavior> _pics;
        private List<SafeButtonController> _safeButtonControllers;
        private Animator _animator;
        private GameObject _safePuzzle;
        #endregion

        private void Start()
        {
            _safePuzzle = GameObject.Find("SafePuzzle");
            _animator = gameObject.GetComponent<Animator>();
            var buttons = Object.FindObjectsOfType<SafeButtonController>();
            _safeButtonControllers = new List<SafeButtonController>();
            foreach (var button in buttons)
            {
                _safeButtonControllers.Add(button);
            }
            _safeButtonControllers.ForEach(s => s.Inicialize());
            _pics = new List<SafeBitePicBehavior>();
            var pics = Object.FindObjectsOfType<SafeBitePicBehavior>();
            foreach(var pic in pics)
            {
                _pics.Add(pic);
            }            
        }

        public void CheckRotate()
        {
            if (!_pics.Exists(s => 0 != Mathf.RoundToInt(s.transform.rotation.eulerAngles.z)))
            {
                _safeButtonControllers.ForEach(s => s.gameObject.SetActive(false));
                _animator.SetBool("Active", true);
                Destroy(_safePuzzle , 5);
            }
        }


    }
}

