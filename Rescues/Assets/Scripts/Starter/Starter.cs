using UnityEngine;
using System.Collections.Generic;

public class Starter : MonoBehaviour
{
    public List<ManagerBase> managers = new List<ManagerBase>();

    private void Awake()
    {
        foreach(var managerBase in managers)
        {
            ToolBox.Add(managerBase);
        }
    }
}
