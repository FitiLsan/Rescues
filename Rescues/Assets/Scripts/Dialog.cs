using UnityEngine;
using System.Collections;


public class Dialog : Interactable
{
    public GameObject DialogSprite;

    public override void Interact(Transform go)
    {
		var test = 1;
        _dialogSprite.SetActive(!_dialogSprite.activeInHierarchy);
    }
}
