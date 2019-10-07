using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected float interactiveDistance;
    [SerializeField] protected LayerMask playerMask;

    // Update is called once per frame
    protected virtual void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, interactiveDistance, playerMask))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Collider2D trans = Physics2D.OverlapCircle(transform.position, interactiveDistance, playerMask);
                Interact(trans.transform);
            }
        }
    }

    public virtual void Interact(Transform go)
    {

    }

    // Для маза факи игрока
    public virtual void Interact(Transform go, KeyCode key)
    {

    }
}
