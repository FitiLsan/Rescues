using UnityEngine;
using System.Collections;


public class Dialog : Interactable
{
    public GameObject _dialogSprite;

    public override void Interact(Transform go)
    {
        _dialogSprite.SetActive(!_dialogSprite.activeInHierarchy);
    }
}
