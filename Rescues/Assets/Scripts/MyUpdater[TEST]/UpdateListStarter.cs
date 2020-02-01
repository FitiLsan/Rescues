using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateListStarter : MonoBehaviour
{
    
    void Update()
    {
        
        for(int i = 0; i < UpdateListCreator.updateList.Count; i++)
        {

            UpdateListCreator.updateList[i].GameControllerScriptFunctions();

        }


    }
}
