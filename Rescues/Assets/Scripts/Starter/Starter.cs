using UnityEngine;
using System.Collections.Generic;


namespace Rescues
{
    public class Starter : MonoBehaviour
    {
        public List<ManagerBase> managers = new List<ManagerBase>();

        private void Awake()
        {
            foreach (var managerBase in managers)
            {
                ToolBox.Add(managerBase);
            }
        }
    }
}
