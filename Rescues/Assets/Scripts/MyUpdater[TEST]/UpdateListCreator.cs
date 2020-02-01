using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UpdateListCreator : MonoBehaviour
{

    public static List<UpdateListCreator> updateList = new List<UpdateListCreator>();


    private void OnEnable()
    {
        updateList.Add(this);
    }

    private void OnDisable()
    {
        updateList.Remove(this);
    }

    public virtual void GameControllerScriptFunctions()
    {

    }


}
