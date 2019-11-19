﻿using UnityEngine;
using UnityEditor;

namespace Rescues
{
    [CreateAssetMenu(fileName = "ManagerEvents", menuName = "Managers/ManagersEvents")]

    public class ManagerEvents : ManagerBase
    {
        public GameObject prefab;

        public void CreatePrefab(Vector3 pos)
        {
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }
}